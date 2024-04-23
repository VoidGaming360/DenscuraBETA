using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.model
{
    public class Treatment
    {
        public int Id { get; set; }
        public string DentistId { get; set; }
        public string PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
        public string TreatmentPlan { get; set; }
        public string Description { get; set; }
        public string Prescription { get; set; }
        public string Diagnosis { get; set; }
        public int Costs { get; set; }
        
    }
}
