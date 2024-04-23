namespace SolisDensCuraBETA.model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; } // Foreign key to ApplicationUser
        public string ReceiverId { get; set; } // Foreign key to ApplicationUser
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        // Navigation properties
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
