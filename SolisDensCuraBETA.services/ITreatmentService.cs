using SolisDensCuraBETA.model;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface ITreatmentService
    {
        IEnumerable<Treatment> GetAllTreatments(Expression<Func<Treatment, bool>> filter = null,
                                               Func<IQueryable<Treatment>, IOrderedQueryable<Treatment>> orderBy = null,
                                               string includeProperties = "");
        Task<Treatment> GetTreatmentByIdAsync(int id);
        Task AddTreatmentAsync(Treatment treatment);
        Task UpdateTreatmentAsync(Treatment treatment);
        Task DeleteTreatmentAsync(int id);
        Task<int> CountTreatmentsAsync(Expression<Func<Treatment, bool>> predicate = null);
    }
}
