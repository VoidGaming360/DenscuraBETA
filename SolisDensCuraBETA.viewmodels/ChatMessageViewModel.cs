using SolisDensCuraBETA.model;

namespace SolisDensCuraBETA.viewmodels
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        public ChatMessageViewModel() { }

        public ChatMessageViewModel(ChatMessage model, string senderName, string receiverName)
        {
            Id = model.Id;
            SenderId = model.SenderId;
            ReceiverId = model.ReceiverId;
            Message = model.Message;
            SentAt = model.SentAt;
            SenderName = senderName ?? throw new ArgumentNullException(nameof(senderName));
            ReceiverName = receiverName ?? throw new ArgumentNullException(nameof(receiverName));
        }

        // Method to format the SentAt property for display
        public string GetFormattedSentAt()
        {
            // Example: "15 Apr 2024 10:30 AM"
            return SentAt.ToString("dd MMM yyyy hh:mm tt");
        }
    }
}
