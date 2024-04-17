using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
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

        void InsertAppointment(AppointmentViewModel Appointment);
        void DeleteAppointment(int id);
    }
}
