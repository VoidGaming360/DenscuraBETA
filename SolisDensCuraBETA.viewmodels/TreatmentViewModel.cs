using SolisDensCuraBETA.model;
using System;

namespace SolisDensCuraBETA.viewmodels
{
    public class TreatmentViewModel
    {
        public int Id { get; set; }
        public string DentistId { get; set; }
        public string PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public string Description { get; set; }
        public string Prescription { get; set; }
        public string Diagnosis { get; set; }
        public int Costs { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }

        public TreatmentViewModel()
        {
        }

        public TreatmentViewModel(Treatment model)
        {
            Id = model.Id;
            AppointmentId = model.AppointmentId;
            Notes = model.Notes;
            TreatmentPlan = model.TreatmentPlan;
            Description = model.Description;
            Prescription = model.Prescription;
            Diagnosis = model.Diagnosis;
            Costs = model.Costs;
        }

        public static Treatment ConvertViewModelToTreatment(TreatmentViewModel viewModel)
        {
            return new Treatment
            {
                Id = viewModel.Id,
                AppointmentId = viewModel.AppointmentId,
                Notes = viewModel.Notes,
                TreatmentPlan = viewModel.TreatmentPlan,
                Description = viewModel.Description,
                Prescription = viewModel.Prescription,
                Diagnosis = viewModel.Diagnosis,
                Costs = viewModel.Costs,
            };
        }
    }
}
