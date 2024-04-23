using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface IAppointment
    {
        PagedResult<AppointmentViewModel> GetAll(int pageNumber, int pageSize);
        PagedResult<AppointmentViewModel> GetAllForUser(string userId, int pageNumber, int pageSize);


        AppointmentViewModel GetById(int AppointmentId);

        void RespondToAppointment(int appointmentId, string status);

        public IEnumerable<Appointment> GetAppointmentsForDentist(string currentUserId);

        public void UpdateAppointment(Appointment appointment);

        void InsertAppointment(AppointmentViewModel Appointment);
        void DeleteAppointment(int id);

        IEnumerable<Appointment> GetConfirmedAppointments(string dentistId);
    }
}
