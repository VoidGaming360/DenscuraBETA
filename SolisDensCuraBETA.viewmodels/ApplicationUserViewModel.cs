﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolisDensCuraBETA.model;


namespace SolisDensCuraBETA.viewmodels
{
    public class ApplicationUserViewModel
    {
        public List<ApplicationUser> Dentists { get; set; } = new List<ApplicationUser>();

        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; } 
        public bool IsDentist { get; set; }
        public string Specialist { get; set; }

        public ApplicationUserViewModel() 
        {
        
        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            Name = user.Name;
            Address = user.Address;
            Gender = user.Gender;
            IsDentist = user.IsDentist;
            Specialist = user.Specialist;
            UserName = user.UserName;
            Email = user.Email;
            
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                Name = user.Name,
                Address = user.Address,
                Gender = user.Gender,
                IsDentist = user.IsDentist,
                Specialist = user.Specialist,
                Email = user.Email,
                UserName = user.UserName

            };
        }

    }
}
