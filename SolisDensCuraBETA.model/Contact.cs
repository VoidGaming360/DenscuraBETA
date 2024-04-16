namespace SolisDensCuraBETA.model
{
    public class Contact
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}