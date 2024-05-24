using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.services.Interface;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class AppointmentService : IAppointment
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public AppointmentService(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
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
                .Where(a => a.PatientId == userId)
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

        public IEnumerable<Appointment> GetAppointmentsForDentist(string currentUserId)
        {
            return _unitOfWork.GenericRepositories<Appointment>()
                .Where(a => a.SelectedDentistId == currentUserId);
        }

        public PagedResult<AppointmentViewModel> GetAll(int pageNumber, int pageSize)
        {
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
                ReasonForVisit = vm.ReasonForVisit,
            };

            _unitOfWork.GenericRepositories<Appointment>().Add(appointment);
            _unitOfWork.Save();
        }

        public void RespondToAppointment(int appointmentId, string status)
        {
            var appointment = _unitOfWork.GenericRepositories<Appointment>().GetById(appointmentId);
            if (appointment == null)
            {
                throw new ArgumentException("Appointment not found");
            }

            switch (status.ToLower())
            {
                case "confirmed":
                    appointment.AppointmentStatus = AppointmentStatus.confirmed.ToString();
                    break;
                case "denied":
                    appointment.AppointmentStatus = AppointmentStatus.denied.ToString();
                    break;
                case "pending":
                    appointment.AppointmentStatus = AppointmentStatus.pending.ToString();
                    break;
                default:
                    throw new ArgumentException("Invalid appointment status");
            }

            _unitOfWork.Save();
        }

        public IEnumerable<Appointment> GetConfirmedAppointments(string dentistId)
        {
            return _dbContext.Appointments
                .Where(a => a.SelectedDentistId == dentistId && a.AppointmentStatus == AppointmentStatus.confirmed.ToString())
                .ToList();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _unitOfWork.GenericRepositories<Appointment>().Update(appointment);
            _unitOfWork.Save();
        }

        public async Task AddTreatmentToAppointmentAsync(int appointmentId, Treatment treatment)
        {
            var appointment = _unitOfWork.GenericRepositories<Appointment>().GetById(appointmentId);

            if (appointment != null)
            {
                appointment.Treatments.Add(treatment);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        public IEnumerable<Appointment> GetAppointmentsForPatient(string patientId)
        {
            return _unitOfWork.GenericRepositories<Appointment>().GetAll(a => a.PatientId == patientId).ToList();
        }

        public IEnumerable<Treatment> GetTreatmentsForAppointment(int appointmentId)
        {
            var appointment = _unitOfWork.GenericRepositories<Appointment>().GetById(appointmentId);
            return appointment?.Treatments ?? Enumerable.Empty<Treatment>();
        }

        private List<AppointmentViewModel> ConvertModelToViewModelList(List<Appointment> modelList)
        {
            return modelList.Select(x => new AppointmentViewModel(x)).ToList();
        }
    }
}
