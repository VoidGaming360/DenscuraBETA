using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;

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
            try
            {
                // Create a new ChatMessage instance
                var chatMessage = new ChatMessage
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Message = message,
                    SentAt = DateTime.Now // You might want to use UTC time instead
                };

                // Add the chat message to the database
                _unitOfWork.GenericRepositories<ChatMessage>().Add(chatMessage);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception("Failed to send message.", ex);
            }
        }
    }
}