using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IChatHub
    {
        Task SendMessage(string message, string roomId, string userTo, string? user = null);
    }
}
