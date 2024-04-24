using System;

namespace SolisDensCuraBETA.viewmodels
{
    public class CostViewModel
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int TotalCost { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }

        public string PaymentStatus { get; set; } = "Paid";

    }
}
