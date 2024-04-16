using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<Clinic> Clinic { get; set; }
        
        public ContactViewModel(Contact model)
        {
            Id = model.Id;
            Email = model.Email;
            Phone = model.Phone;
            ClinicId = model.ClinicId;
        }

        public ContactViewModel()
        {
        }

        public Contact ConvertViewModel(ContactViewModel model)
        {
            return new Contact
            {
                Id = model.Id,
                Email = model.Email,
                Phone = model.Phone,
                ClinicId = model.ClinicId,
        };
        }

       
    }
    
}

