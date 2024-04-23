using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;

namespace SolisDensCuraBETA.services
{
    public class AppointmentService : IAppointment
    {
        private IUnitOfWork _unitOfWork;
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
            // Retrieve appointments where the SelectedDentistId matches the current user's ID
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

            // Add the appointment to the DbContext
            _unitOfWork.GenericRepositories<Appointment>().Add(appointment);

            // Save changes to the database
            _unitOfWork.Save();
        }

        public void RespondToAppointment(int appointmentId, string status)
        {
            // Retrieve the appointment by its ID
            var appointment = _unitOfWork.GenericRepositories<Appointment>().GetById(appointmentId);
            if (appointment == null)
            {
                throw new ArgumentException("Appointment not found");
            }

            // Update the appointment status
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

            // Save changes
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

        private List<AppointmentViewModel> ConvertModelToViewModelList(List<Appointment> modelList)
        {
            return modelList.Select(x => new AppointmentViewModel(x)).ToList();
        }
    }
}
