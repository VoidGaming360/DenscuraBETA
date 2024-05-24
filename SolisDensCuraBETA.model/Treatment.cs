namespace SolisDensCuraBETA.model
{
    public class Treatment
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public string Description { get; set; }
        public string Prescription { get; set; }
        public string Diagnosis { get; set; }
        public int Costs { get; set; }
        public bool? IsPaid { get; set; } = false;
        public bool IsPressed { get; set; } = false;

        // Foreign key for Appointment
        public int AppointmentId { get; set; }

        // Navigation property for Appointment
        public Appointment Appointment { get; set; }

        public bool GetIsPaid()
        {
            return IsPaid ?? false;
        }
    }
}
