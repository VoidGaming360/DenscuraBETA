using SolisDensCuraBETA.model;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface ITreatmentService
    {
        Appointment GetAppointmentById(int appointmentId);
        void CreateTreatment(TreatmentViewModel model);
        List<Treatment> GetTreatmentsForAppointment(int appointmentId);
        Task CreateTreatmentAsync(Treatment treatment);
        void SaveTreatment(Treatment treatment);
    }
}
