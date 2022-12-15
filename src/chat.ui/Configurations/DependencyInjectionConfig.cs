using Chat.Domain.Hubs;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Services;
using Chat.Domain.Interfaces;
using Chat.Domain.Interfaces.Repositories;
using Domain.Interfaces.ExternalServices;
using Infra.External.ExternalServices;
using FinancialChat.Infra.Data.Repositories;

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
            
            #endregion

            #region repositories
            services.AddScoped<IChatRepository, ChatRepository>();
            #endregion
        }
    }
}
