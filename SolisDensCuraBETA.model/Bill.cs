namespace SolisDensCuraBETA.model
{
    public class Bill
    {
        public int Id { get; set; }
        public int BillNumber { get; set; }
        public ApplicationUser Patient { get; set; }

        public Insurance Insurance { get; set; }
        public int DentistFees { get; set; }
        public decimal MedicineCharge { get; set; }
        public decimal RoomCharge { get; set; }
        public decimal LabCharge { get; set; }
        public decimal Advance {  get; set; }
        public decimal TotalBill { get; set; }

    }
}