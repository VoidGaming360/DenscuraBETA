using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.model
{
    public class Supplies
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Cost {  get; set; }
        public string Description { get; set; }

        public ICollection<SuppliesReport> SuppliesReport { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
