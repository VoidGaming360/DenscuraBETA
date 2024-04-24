using SolisDensCuraBETA.model;
using SolisDensCuraBETA.viewmodels;

namespace SolisDensCuraBETA.services
{
    public class CostService : ICostService
    {
        private readonly ITreatmentService _treatmentService;

        public CostService(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        public IEnumerable<CostViewModel> GetCostsForExistingTreatments()
        {
            var treatments = _treatmentService.GetAllTreatments();

            List<CostViewModel> costs = new List<CostViewModel>();

            foreach (var treatment in treatments)
            {
                // Check if treatment exists
                if (treatment != null)
                {
                    // Calculate total cost
                    int totalCost = CalculateTotalCost(treatment);

                    // Format cost information into invoice format
                    var costViewModel = new CostViewModel
                    {
                        TreatmentId = treatment.Id,
                        TotalCost = totalCost,
                        // Populate other properties as needed for invoice format
                    };

                    costs.Add(costViewModel);
                }
            }

            return costs;
        }

        private int CalculateTotalCost(Treatment treatment)
        {
            // Calculate total cost based on individual costs in the treatment
            int totalCost = 0;

            // Include the costs from the Treatment table
            totalCost += treatment.Costs;

            // Add any additional costs from other sources if needed

            return totalCost;
        }
    }
}
