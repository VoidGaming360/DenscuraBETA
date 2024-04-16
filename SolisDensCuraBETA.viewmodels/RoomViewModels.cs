using Microsoft.EntityFrameworkCore.Migrations;
using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class RoomViewModels
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public RoomViewModels()
        {

        }

        public RoomViewModels(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            Type = model.Type;
            Status = model.Status;
            ClinicId = model.ClinicId;
            Clinic = model.Clinic;
        }

        public Room ConvertViewModel(RoomViewModels model)
        {
            return new Room
            {
                Id = model.Id,
                RoomNumber = model.RoomNumber,
                Type = model.Type,
                Status = model.Status,
                ClinicId = model.ClinicId,
                Clinic = model.Clinic,
            };
        }
    }

    
}
