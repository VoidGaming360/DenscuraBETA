namespace SolisDensCuraBETA.model
{
    public class Room
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public string Type { get; set;}

        public string Status { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}