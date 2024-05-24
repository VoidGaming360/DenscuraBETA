using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SendMessageAsync(string senderId, string receiverId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.UtcNow
            };

            await _unitOfWork.GenericRepositories<ChatMessage>().AddAsync(chatMessage);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(string userId1, string userId2)
        {
            return await _unitOfWork.GenericRepositories<ChatMessage>()
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
