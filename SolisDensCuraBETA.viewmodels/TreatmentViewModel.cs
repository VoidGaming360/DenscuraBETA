using System.ComponentModel.DataAnnotations;

namespace SolisDensCuraBETA.viewmodels
{
    public class TreatmentViewModel
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime? AppointmentDate { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Treatment Plan")]
        public string TreatmentPlan { get; set; }

        public string Description { get; set; }

        public string Prescription { get; set; }

        public string Diagnosis { get; set; }

        [Required]
        public int Costs { get; set; }
    }
}