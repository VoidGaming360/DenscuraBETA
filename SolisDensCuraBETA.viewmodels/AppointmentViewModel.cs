﻿using SolisDensCuraBETA.model;


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

        public string ReasonForVisit { get; set; }
        public DateTime? AppointmentDate { get; set; }  


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
            ReasonForVisit = model.ReasonForVisit;
            AppointmentDate = model.AppointmentDate;
        }

        public static Appointment ConvertViewModelToAppointment(AppointmentViewModel viewModel)
        {
            return new Appointment
            {
                Id = viewModel.Id,
                Number = viewModel.Number,
                Type = viewModel.Type,
                RequestedTime = viewModel.RequestedTime,
                Description = viewModel.Description,
                AppointmentStatus = viewModel.AppointmentStatus,
                ReasonForVisit = viewModel.ReasonForVisit,
                AppointmentDate = viewModel.AppointmentDate
            };
        }
    }
}
