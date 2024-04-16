using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class ClinicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public ClinicViewModel() { }

        public ClinicViewModel(Clinic model)
        {
            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            City = model.City;
            Country = model.Country;
        }
        public Clinic ConvertViewModel(ClinicViewModel model)
        {
            return new Clinic{ 
            Id = model.Id,
            Name = model.Name,
            Type = model.Type,
            City = model.City,
            Country = model.Country,
            };
        }

    }
}
