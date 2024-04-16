using System;
using SolisDensCuraBETA.model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class SuppliesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }

        public SuppliesViewModel() 
        {
        }

        public SuppliesViewModel(Supplies model)
        {
            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            Cost = model.Cost;
            Description = model.Description;
        }

        public Supplies ConvertViewModel(SuppliesViewModel model)
        {
            return new Supplies
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Cost = model.Cost,
                Description = model.Description
            };
        }

    }
}
