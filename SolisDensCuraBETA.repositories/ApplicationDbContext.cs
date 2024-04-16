using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.repositories
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Supplies> Supply { get; set; }
        public DbSet<SuppliesReport> SuppliesReports { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TestPrice> TestPrices { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Timing> Timings { get; set; }


    }
}
