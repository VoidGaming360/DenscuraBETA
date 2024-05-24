namespace SolisDensCuraBETA.model
{
    public class Cost
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }  // Reference to the Treatment
        public Treatment Treatment { get; set; }  // Navigation property
        public int TotalCost { get; set; }  // Total cost including all treatment costs

        public string CustomerName { get; set; }
        public DateTime Date { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;  // Default payment status
    }

    public enum PaymentStatus
    {
        Paid,
        Pending,
        Overdue
    }
}
