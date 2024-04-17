using Microsoft.Extensions.Hosting;
using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime RequestedTime { get; set; }
        public string Description { get; set; }
        public string AppointmentStatus { get; set; }
        public string PatientId { get; set; }

        public string SelectedDentistId { get; set; }


        public AppointmentViewModel()
        {
        }

        public AppointmentViewModel(Appointment model)
        {
            Id = model.Id;
            Number = model.Number;
            Type = model.Type;
            RequestedTime = model.RequestedTime;
            Description = model.Description;
            AppointmentStatus = model.AppointmentStatus;
        }

        public Appointment ConvertViewModel(AppointmentViewModel model)
        {
            return new Appointment
            {
                Id = model.Id,
                Number = model.Number,
                Type = model.Type,
                RequestedTime = model.RequestedTime,
                Description = model.Description,
                AppointmentStatus = model.AppointmentStatus
            };
        }
    }
}
