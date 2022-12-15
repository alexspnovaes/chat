using Chat.Domain.Interfaces.Services;
using Chat.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.UI.Controllers
{
    public class RoomController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public RoomController(IChatService chatService, IUserService userService, IConfiguration configuration)
        {
            _chatService = chatService;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View(await _chatService.GetRoomsAsync());
        }

        public async Task<IActionResult> Messages(string id)
        {
            var limit = Convert.ToInt32(_configuration.GetSection("Configurations").GetSection("ChatMessageLimit").Value);
            var messages = await _chatService.GetMessagesAsync(id, 0, limit - 1);
            var onlineUsers = await _userService.GetOnlineUsersAsync(id);

            var model = new RoomViewModel
            {
                Messages = messages,
                Users = onlineUsers
            };

            ViewBag.RoomId = id;
            return View(model);
        }
    }
}
