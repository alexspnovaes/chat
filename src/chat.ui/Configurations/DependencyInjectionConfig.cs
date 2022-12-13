using Chat.Domain.Hubs;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Services;
using Chat.Domain.Interfaces;
using Chat.domain.Interfaces.Services;
using Chat.domain.Services;
using Infra.External.ExternalServices;
using Infra.External.Interfaces;
using Chat.Domain.Interfaces.Repositories;

namespace Chat.UI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotifyStockService, NotifyStockService>();
            services.AddScoped<IStockApiService, StockApiService>();
            services.AddScoped<IStockApiExternalService, StockApiExternalService>();
            services.AddScoped<IChatHub, ChatHub>();
            #endregion

            #region repositories
            services.AddScoped<IChatRepository, RoomRepository>();
            #endregion
        }
    }
}
