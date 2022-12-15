using Chat.Domain.Hubs;
using Chat.Infra.Data.Context;
using Chat.UI.Configuration;
using Chat.UI.Consumers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var mqConfig = builder.Configuration.GetSection("RabbitMqConfig");
builder.Services.Configure<RabbitMqConfiguration>(mqConfig);
builder.Services.AddHostedService<StockMessageConsumer>();

// Add services to the container.
builder.Services.AddControllersWithViews();

string? redisConnectionUrl = null;
var redisEndpointUrl = (Environment.GetEnvironmentVariable("REDIS_ENDPOINT_URL") ?? "localhost:6379").Split(':');
var redisHost = redisEndpointUrl[0];
var redisPort = redisEndpointUrl[1];

var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
if (redisPassword != null)
{
    redisConnectionUrl = $"{redisHost}:{redisPort},password={redisPassword}";
}
else
{
    redisConnectionUrl = $"{redisHost}:{redisPort}";
}

var redis = ConnectionMultiplexer.Connect(redisConnectionUrl);


builder.Services.AddSingleton<IConnectionMultiplexer>(redis);


builder.Services.AddDbContext<ChatContext>(
               options => options.
               UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ChatContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequiredUniqueChars = 3;
});


builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = redisConnectionUrl;
    option.InstanceName = "RedisInstance";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "Chat";
});

builder.Services.AddSignalR();

builder.Services.RegisterServices();
builder.Services.RegisterMappings();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();


app.MapHub<ChatHub>("/chatHub");

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
