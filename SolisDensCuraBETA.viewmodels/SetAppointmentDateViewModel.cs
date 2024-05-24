using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class SetAppointmentDateViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }
    }
}
