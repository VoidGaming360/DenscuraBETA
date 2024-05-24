using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        Task<IEnumerable<Treatment>> GetTreatmentsByAppointmentIdsAsync(IEnumerable<int> appointmentIds);
    }
}
