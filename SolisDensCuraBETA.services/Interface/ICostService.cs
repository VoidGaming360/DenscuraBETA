using SolisDensCuraBETA.model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services.Interface
{
    public interface ICostService
    {
        Task AddCostAsync(Cost cost);
        Task<Cost> GetCostByIdAsync(int id);
        Task UpdateCostAsync(Cost cost);
        Task<IEnumerable<Cost>> GetAllCostsAsync();
        Task<IEnumerable<Cost>> GetCostsByTreatmentIdsAsync(IEnumerable<int> treatmentIds);
    }
}
