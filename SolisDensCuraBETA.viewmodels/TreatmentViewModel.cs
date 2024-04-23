namespace SolisDensCuraBETA.viewmodels
{
    public class TreatmentViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public string Description { get; set; }
        public string Prescription { get; set; }
        public string Diagnosis { get; set; }
        public int Costs { get; set; }

        // Additional properties if needed in the view
        public string DisplayCosts => $"${Costs}"; // Format costs for display
        public string DisplayNumber => $"Treatment #{Number}"; // Format number for display
    }
}