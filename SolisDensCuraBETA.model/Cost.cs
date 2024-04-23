using System;

namespace SolisDensCuraBETA.model
{
    public class Cost
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }  // Reference to the Treatment
        public Treatment Treatment { get; set; }  // Navigation property
        public int TotalCost { get; set; }  // Total cost including all treatment costs

        // Other fields related to billing information such as customer name, date, etc.
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }

        public string PaymentStatus { get; set; }
        // Add more billing-related properties as needed
    }

    
}
namespace SolisDensCuraBETA.model
{
    public enum PaymentStatus
    {
        Paid,
        Pending,
        Overdue
    }
}