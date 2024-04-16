using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface IRoom
    {
        PagedResult<RoomViewModels> GetAll(int pageNumber, int pageSize);

        RoomViewModels GetById(int RoomId);

        void UpdateRoom(RoomViewModels Room);
        void InsertRoom(RoomViewModels Room);
        void DeleteRoom(int id);
    }
}
