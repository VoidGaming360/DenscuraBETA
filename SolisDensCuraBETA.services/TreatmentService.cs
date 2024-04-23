using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            // Access the GenericRepository<Appointment> through the IUnitOfWork
            return _unitOfWork.GenericRepositories<Appointment>().GetById(appointmentId);
        }



        public void CreateTreatment(TreatmentViewModel vm)
        {
            var treatment = new Treatment
            {
                // Map properties from the view model to the treatment entity
                AppointmentId = vm.AppointmentId,
                Notes = vm.Notes,
                TreatmentPlan = vm.TreatmentPlan,
                Description = vm.Description,
                Prescription = vm.Prescription,
                Diagnosis = vm.Diagnosis,
                Costs = vm.Costs,
                // Map other properties as needed
            };

            // Add the treatment to the repository and save changes
            _unitOfWork.GenericRepositories<Treatment>().Add(treatment);
            _unitOfWork.Save();
        }

        public List<Treatment> GetTreatmentsForAppointment(int appointmentId)
        {
            // Retrieve treatments associated with the given appointmentId
            return _unitOfWork.GenericRepositories<Treatment>()
                                .Where(t => t.AppointmentId == appointmentId)
                                .ToList();
        }

        public async Task CreateTreatmentAsync(Treatment treatment)
        {
            _unitOfWork.GenericRepositories<Treatment>().Add(treatment);
            await _unitOfWork.GenericRepositories<Treatment>().SaveChangesAsync(); // Assuming you have a method like this for asynchronous saving changes
        }

        public void SaveTreatment(Treatment treatment)
        {
            // Access the GenericRepository<Treatment> through the IUnitOfWork
            var treatmentRepository = _unitOfWork.GenericRepositories<Treatment>();

            // Add or update the treatment in the repository
            if (treatment.Id == 0)
            {
                // If the treatment ID is 0, it means it's a new treatment
                treatmentRepository.Add(treatment);
            }
            else
            {
                // If the treatment ID is not 0, it means it's an existing treatment
                treatmentRepository.Update(treatment);
            }

            // Save changes to the database
            _unitOfWork.Save();
        }


    }
}
