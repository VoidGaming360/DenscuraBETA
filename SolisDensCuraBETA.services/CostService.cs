using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class CostService : ICostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCostAsync(Cost cost)
        {
            await _unitOfWork.GenericRepositories<Cost>().AddAsync(cost);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Cost> GetCostByIdAsync(int id)
        {
            return await _unitOfWork.GenericRepositories<Cost>().GetByIdAsync(id);
        }

        public async Task UpdateCostAsync(Cost cost)
        {
            _unitOfWork.GenericRepositories<Cost>().Update(cost);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cost>> GetAllCostsAsync()
        {
            return await _unitOfWork.GenericRepositories<Cost>().GetAllAsync();
        }

        public async Task<IEnumerable<Cost>> GetCostsByTreatmentIdsAsync(IEnumerable<int> treatmentIds)
        {
            return await Task.Run(() =>
                _unitOfWork.GenericRepositories<Cost>().GetAll(c => treatmentIds.Contains(c.TreatmentId)).ToList()
            );
        }
    }
}
