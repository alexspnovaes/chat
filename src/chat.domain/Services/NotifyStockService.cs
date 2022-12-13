using Chat.Domain.Hubs;
using Chat.Domain.Interfaces;
using Chat.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Chat.Domain.Services
{
    public class NotifyStockService : INotifyStockService
    {
        private readonly IChatHub _chatHub;

        public NotifyStockService(IChatHub chatHub)
        {
            _chatHub = chatHub;
        }

        public async Task NotifyRoomUserAsync(string code, string userId, string roomId, string value)
        {
            var message = string.Concat(code, " quote is ", value, " per shre");
            await _chatHub.SendMessage(message, roomId,"All", userId);
        }
    }
}
