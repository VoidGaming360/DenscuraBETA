using Microsoft.Extensions.Hosting;
using SolisDensCuraBETA.model;
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
    public class SuppliesService : ISupplies
    {
        private IUnitOfWork _unitOfWork;

        public SuppliesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteSupplies(int id)
        {
            var model = _unitOfWork.GenericRepositories<Supplies>().GetById(id);
            _unitOfWork.GenericRepositories<Supplies>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<SuppliesViewModel> GetAll(int pageNumber, int pageSize)
        {
            var SuppliesViewModel = new SuppliesViewModel();
            int totalCount;
            List<SuppliesViewModel> vmList = new List<SuppliesViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepositories<Supplies>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepositories<Supplies>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<SuppliesViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public SuppliesViewModel GetById(int SupplyId)
        {
            var model = _unitOfWork.GenericRepositories<Supplies>().GetById(SupplyId);
            var vm = new SuppliesViewModel(model);
            return vm;
        }

        public void InsertSupplies(SuppliesViewModel Supply)
        {
            var model = new SuppliesViewModel().ConvertViewModel(Supply);
            _unitOfWork.GenericRepositories<Supplies>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateSupplies(SuppliesViewModel Supply)
        {
            var model = new SuppliesViewModel().ConvertViewModel(Supply);
            var ModelById = _unitOfWork.GenericRepositories<Supplies>().GetById(model.Id);

            ModelById.Id = Supply.Id;
            ModelById.Name = Supply.Name;
            ModelById.Type = Supply.Type;
            ModelById.Cost = Supply.Cost;
            ModelById.Description = Supply.Description;

            _unitOfWork.GenericRepositories<Supplies>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<SuppliesViewModel> ConvertModelToViewModelList(List<Supplies> modelList)
        {
            return modelList.Select(x => new SuppliesViewModel(x)).ToList();
        }
    }
}
