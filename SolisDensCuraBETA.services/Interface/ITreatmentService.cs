using SolisDensCuraBETA.model;
using System.Linq.Expressions;
namespace SolisDensCuraBETA.services.Interface
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
