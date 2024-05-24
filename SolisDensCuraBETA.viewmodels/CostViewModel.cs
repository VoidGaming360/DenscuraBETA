using System;
using System.ComponentModel.DataAnnotations;

namespace SolisDensCuraBETA.viewmodels
{
    public class CostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Treatment ID")]
        public int TreatmentId { get; set; }

        [Display(Name = "Total Cost")]
        public int TotalCost { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }
    }
}
