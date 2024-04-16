using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public class ClinicService : IClinic
    {
        private IUnitOfWork _unitOfWork;

        public ClinicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteClinic(int id)
        {
            var model = _unitOfWork.GenericRepositories<Clinic>().GetById(id);
            _unitOfWork.GenericRepositories<Clinic>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<ClinicViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount;
            List<ClinicViewModel> vmList = new List<ClinicViewModel>();

            try
            {
                int excludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepositories<Clinic>().GetAll()
                    .Skip(excludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepositories<Clinic>().GetAll().Count();

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ClinicViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public IEnumerable GetAll()
        {
            var clinicList = _unitOfWork.GenericRepositories<Clinic>().GetAll();
            return clinicList;

        }

        public ClinicViewModel GetById(int ClinicId)
        {
            var model = _unitOfWork.GenericRepositories<Clinic>().GetById(ClinicId);
            var vm = new ClinicViewModel(model);
            return vm;
        }

        public ClinicViewModel GetClinicById(int ClinicId) 
        {
            var model = _unitOfWork.GenericRepositories<Clinic>().GetById(ClinicId);
            var vm = new ClinicViewModel(model);
            return vm;
        }

        public void InsertClinic(ClinicViewModel clinic)
        {
            var model = new ClinicViewModel().ConvertViewModel(clinic);
            _unitOfWork.GenericRepositories<Clinic>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateClinic(ClinicViewModel clinic)
        {
            var model = new ClinicViewModel().ConvertViewModel(clinic);
            var ModelById = _unitOfWork.GenericRepositories<Clinic>().GetById(model.Id);

            ModelById.Id = clinic.Id;
            ModelById.Name = clinic.Name;
            ModelById.Type = clinic.Type;
            ModelById.City = clinic.City;
            ModelById.Country = clinic.Country;

            _unitOfWork.GenericRepositories<Clinic>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<ClinicViewModel> ConvertModelToViewModelList(List<Clinic> modelList)
        {
            return modelList.Select(x => new ClinicViewModel(x)).ToList();
        }
    }
}
