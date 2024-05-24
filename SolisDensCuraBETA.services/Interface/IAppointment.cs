using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using SolisDensCuraBETA.model;

namespace SolisDensCuraBETA.services.Interface
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
        IEnumerable<Appointment> GetAppointmentsForPatient(string patientId);
    }
}
