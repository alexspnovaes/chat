using Chat.Domain.Models;

namespace Chat.UI.Models
{
    public class RoomViewModel
    {
        public List<MessageModel> Messages { get; set; }
        public List<string> Users { get; set; }
    }
}