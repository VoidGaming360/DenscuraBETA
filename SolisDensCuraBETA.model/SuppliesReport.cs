using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.model
{
    public class SuppliesReport
    {
        public int Id { get; set; }
        public Supplier Supplier {  get; set; }

        public Supplies Supplies { get; set; }
        public string Company {  get; set; }
        public int Quantity {  get; set; }
        public DateTime ManufactureDate {  get; set; }
        public DateTime ExpirationDate {  get; set; }
        public string Country {  get; set; }
    }
}
