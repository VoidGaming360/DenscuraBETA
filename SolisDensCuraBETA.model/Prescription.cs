using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.model
{
    public class Prescription
    {
        public int Id { get; set; }
        public Supplies Supplies { get; set; } 
        //will probably need to distinguish supply as in items and meds in the future
        public PatientReport PatientReport { get; set; }
    }
}
