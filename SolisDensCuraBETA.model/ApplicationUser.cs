using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.model
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string Nationality { get; set; }
        public string Address {  get; set; }

        public DateTime DOB {  get; set; }

        public string Specialist { get; set; }

        public bool IsDentist { get; set; }

        public string PictureUri { get; set; }

        // Navigation property for messages sent by this user
        public ICollection<ChatMessage> SentMessages { get; set; }

        // Navigation property for messages received by this user
        public ICollection<ChatMessage> ReceivedMessages { get; set; }

        //public Department Department { get; set; }
        [NotMapped] 
        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Treatment> Treatments { get; set; }
        
        public ICollection<Payroll> Payrolls { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}

namespace SolisDensCuraBETA.model
{
    public enum Gender
    {
        Male,Female,Other
    }
}