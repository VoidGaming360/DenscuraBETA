using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SolisDensCuraBETA.services
{
    public class AppointmentService : IAppointment
    {
        private IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteAppointment(int id)
        {
            var model = _unitOfWork.GenericRepositories<Appointment>().GetById(id);
            _unitOfWork.GenericRepositories<Appointment>().Delete(model);
            _unitOfWork.Save();
        }
        public PagedResult<AppointmentViewModel> GetAllForUser(string userId, int pageNumber, int pageSize)
        {
            var totalCount = _unitOfWork.GenericRepositories<Appointment>()
                .Count(a => a.PatientId == userId);

            var appointments = _unitOfWork.GenericRepositories<Appointment>()
                .Where(a => a.PatientId == userId)  // Ensure that 'PatientId' is a string property
                .OrderByDescending(a => a.RequestedTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var appointmentViewModels = appointments.Select(a => new AppointmentViewModel(a)).ToList();

            return new PagedResult<AppointmentViewModel>
            {
                Data = appointmentViewModels,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public PagedResult<AppointmentViewModel> GetAll(int pageNumber, int pageSize)
        {
            var AppointmentViewModel = new AppointmentViewModel();
            int totalCount;
            List<AppointmentViewModel> vmList = new List<AppointmentViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepositories<Appointment>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepositories<Appointment>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<AppointmentViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public AppointmentViewModel GetById(int AppointmentId)
        {
            var model = _unitOfWork.GenericRepositories<Appointment>().GetById(AppointmentId);
            var vm = new AppointmentViewModel(model);
            return vm;
        }

        public void InsertAppointment(AppointmentViewModel vm)
        {
            var appointment = new Appointment
            {
                Description = vm.Description,
                Number = vm.Number,
                RequestedTime = vm.RequestedTime,
                Type = vm.Type,
                PatientId = vm.PatientId,
                SelectedDentistId = vm.SelectedDentistId,
                AppointmentStatus = AppointmentStatus.pending.ToString(),
                
            };

            // Add the appointment to the DbContext
            _unitOfWork.GenericRepositories<Appointment>().Add(appointment);

            // Save changes to the database
            _unitOfWork.Save();
        }

        private List<AppointmentViewModel> ConvertModelToViewModelList(List<Appointment> modelList)
        {
            return modelList.Select(x => new AppointmentViewModel(x)).ToList();
        }
    }
}
