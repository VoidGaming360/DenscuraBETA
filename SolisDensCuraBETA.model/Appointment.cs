namespace SolisDensCuraBETA.model
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Number {  get; set; }
        public string Type { get; set; }
        public DateTime RequestedTime { get; set; }
        public string Description {  get; set; }
        public string AppointmentStatus { get; set; }
        public string SelectedDentistId { get; set; }
        public string PatientId { get; set; }
    }
}

namespace SolisDensCuraBETA.model
{
    public enum AppointmentStatus
    {
        confirmed, denied, pending
    }
}