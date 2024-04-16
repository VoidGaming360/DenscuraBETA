using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]

    [Authorize(Roles = "Patient, Dentist")]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Action for patient to create appointment request
        public IActionResult Create()
        {
            // Logic to display form for creating appointment request
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            // Logic to save appointment request and notify dentist
            appointment.AppointmentStatus = "pending";
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            // Send notification to dentist
            return RedirectToAction("Index", "Home"); // Redirect to home page
        }

        // Action for dentist to view appointment requests
        public async Task<IActionResult> ViewAppointments()
        {
            // Retrieve the current dentist ID from the claims
            var currentUser = await _userManager.GetUserAsync(User);

            // Query appointments for the current dentist
            var appointments = _context.Appointments
                .Where(a => a.DentistId.ToString() == currentUser.Id)
                .ToList();

            return View(appointments);
        }

        [HttpPost]
        public IActionResult RespondToAppointment(int appointmentId, string status)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.AppointmentStatus = status;
                _context.SaveChanges();
                // Send notification to patient
            }
            return RedirectToAction("ViewAppointments");
        }
    }
}
