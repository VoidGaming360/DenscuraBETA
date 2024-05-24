using SolisDensCuraBETA.model;

namespace SolisDensCuraBETA.viewmodels
{
    public class ChatMessageViewModel
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; } // Add ReceiverName for displaying the receiver
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public string ReceiverId { get; set; } // Add ReceiverId for referencing the receiver
    }
}

