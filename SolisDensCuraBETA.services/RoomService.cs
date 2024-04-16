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
    public class RoomService : IRoom
    {
        private IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepositories<Room>().GetById(id);
            _unitOfWork.GenericRepositories<Room>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<RoomViewModels> GetAll(int pageNumber, int pageSize)
        {
            var RoomViewModels = new RoomViewModels();
            int totalCount;
            List<RoomViewModels> vmList = new List<RoomViewModels>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepositories<Room>().GetAll(includeProperties:"Clinic")
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepositories<Room>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<RoomViewModels>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public RoomViewModels GetById(int RoomId)
        {
            var model = _unitOfWork.GenericRepositories<Room>().GetById(RoomId);
            var vm = new RoomViewModels(model);
            return vm;
        }

        public RoomViewModels GetRoomById(int RoomId)
        {
            var model = _unitOfWork.GenericRepositories<Room>().GetById(RoomId);
            var vm = new RoomViewModels(model);
            return vm;
        }

        /*public void InsertClinic(ClinicViewModel clinic) 
        {
            var model = new ClinicViewModel().ConvertViewModel(clinic);
            _unitOfWork.GenericRepositories<Clinic>().Add(model);
            _unitOfWork.Save();
        }



        public void UpdateClinic(ClinicViewModel clinic) 
        {
            var model = new ClinicViewModel().ConvertViewModel(clinic);
            var ModelById = _unitOfWork.GenericRepositories<Clinic>().GetById(model.Id);
            ModelById.Name = clinic.Name;
            ModelById.City = clinic.City;
            ModelById.Country = clinic.Country;
            _unitOfWork.GenericRepositories<Clinic>().Update(ModelById);
            _unitOfWork.Save();
        }
        */

        /*public void InsertRoom(RoomViewModels roomViewModel)
        {
            var room = new Room
            {
                RoomNumber = roomViewModel.RoomNumber,
                Type = roomViewModel.Type, // Set the Type property
                Status = roomViewModel.Status,
                ClinicId = roomViewModel.ClinicId
        };

            _unitOfWork.GenericRepositories<Room>().Add(room);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModels roomViewModel)
        {
            var room = new Room
            {
                Id = roomViewModel.Id,
                RoomNumber = roomViewModel.RoomNumber,
                Type = roomViewModel.Type, // Set the Type property
                Status = roomViewModel.Status,
                ClinicId = roomViewModel.ClinicId
            };

            _unitOfWork.GenericRepositories<Room>().Update(room);
            _unitOfWork.Save();
        } */

        public void InsertRoom(RoomViewModels Room)
        {
            var model = new RoomViewModels().ConvertViewModel(Room);
            _unitOfWork.GenericRepositories<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModels Room)
        {
            var model = new RoomViewModels().ConvertViewModel(Room);
            var ModelById = _unitOfWork.GenericRepositories<Room>().GetById(model.Id);
            ModelById.Type = Room.Type;
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status= Room.Status;
            ModelById.ClinicId = Room.ClinicId;

            _unitOfWork.GenericRepositories<Room>().Update(ModelById);
            _unitOfWork.Save();
        }

        private List<RoomViewModels> ConvertModelToViewModelList(List<Room> modelList)
        {
            return modelList.Select(x => new RoomViewModels(x)).ToList();
        }
    }
}
