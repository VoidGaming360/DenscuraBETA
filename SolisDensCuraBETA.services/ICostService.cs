using SolisDensCuraBETA.viewmodels;

namespace SolisDensCuraBETA.services
{
    public interface ICostService
    {
        public IEnumerable<CostViewModel> GetCostsForExistingTreatments();
    }
}
