using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Treatment> GetAllTreatments(Expression<Func<Treatment, bool>> filter = null,
                                               Func<IQueryable<Treatment>, IOrderedQueryable<Treatment>> orderBy = null,
                                               string includeProperties = "")
        {
            return _unitOfWork.GenericRepositories<Treatment>().GetAll(filter, orderBy, includeProperties).ToList();
        }
        public async Task<Treatment> GetTreatmentByIdAsync(int id)
        {
            return await _unitOfWork.GenericRepositories<Treatment>().GetByIdAsync(id);
        }

        public async Task AddTreatmentAsync(Treatment treatment)
        {
            await _unitOfWork.GenericRepositories<Treatment>().AddAsync(treatment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTreatmentAsync(Treatment treatment)
        {
            _unitOfWork.GenericRepositories<Treatment>().Update(treatment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTreatmentAsync(int id)
        {
            var treatment = await _unitOfWork.GenericRepositories<Treatment>().GetByIdAsync(id);
            if (treatment != null)
            {
                _unitOfWork.GenericRepositories<Treatment>().Delete(treatment);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public Task<int> CountTreatmentsAsync(Expression<Func<Treatment, bool>> predicate = null)
        {
            int count = _unitOfWork.GenericRepositories<Treatment>().Count(predicate);
            return Task.FromResult(count);
        }

    }
}
