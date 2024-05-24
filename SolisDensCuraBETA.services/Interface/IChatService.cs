using SolisDensCuraBETA.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services.Interface
{
    public interface IChatService
    {
        Task SendMessageAsync(string senderId, string receiverId, string message);
        Task<IEnumerable<ChatMessage>> GetMessagesAsync(string userId1, string userId2);
    }
}
