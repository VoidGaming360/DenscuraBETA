using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Implementation;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class DentistService : IDentistService
    {
        private IUnitOfWork _unitOfWork;

        public DentistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTiming(TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            _unitOfWork.GenericRepositories<Timing>().Add(model);
            _unitOfWork.Save();
        }

        public void DeleteTiming(int TimingId)
        {
            var model = _unitOfWork.GenericRepositories<Timing>().GetById(TimingId);
            _unitOfWork.GenericRepositories<Timing>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new TimingViewModel();
            int totalCount;
            List<TimingViewModel> vmList = new List<TimingViewModel>();

            try
            {
                int excludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepositories<Timing>().GetAll()
                    .Skip(excludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepositories<Timing>().GetAll().Count();

                vmList = (List<TimingViewModel>)ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<TimingViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public IEnumerable<TimingViewModel> GetAll()
        {
            var TimingList = _unitOfWork.GenericRepositories<Timing>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(TimingList);
            return vmList;
        }

        private IEnumerable<TimingViewModel> ConvertModelToViewModelList(List<Timing> timingList)
        {
            return timingList.Select(x => new TimingViewModel(x)).ToList();
        }

        public TimingViewModel GetTimingById(int TimingId)
        {
            var model = _unitOfWork.GenericRepositories<Timing>().GetById(TimingId);
            var vm = new TimingViewModel(model);
            return vm;
        }

        public void UpdateTiming(TimingViewModel timing)
        {
            var model = new TimingViewModel().ConvertViewModel(timing);
            var ModelById = _unitOfWork.GenericRepositories<Timing>().GetById(model.Id);
            ModelById.Id = timing.Id;
            ModelById.DoctorID = timing.DoctorID;
            ModelById.Status = timing.Status;
            ModelById.Duration = timing.Duration;
            ModelById.MorningShiftStartTime = timing.MorningShiftStartTime;
            ModelById.MorningShiftEndTime = timing.MorningShiftEndTime;
            ModelById.AfternoonShiftStartTime = timing.AfternoonShiftStartTime;
            ModelById.AfternoonShiftEndTime = timing.AfternoonShiftEndTime;

            _unitOfWork.GenericRepositories<Timing>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<TimingViewModel> ConvertModelToViewModelList(List<TimingViewModel> modelList)
        {
            return modelList.Select(x => new TimingViewModel(x)).ToList();
        }
    }

    
}
