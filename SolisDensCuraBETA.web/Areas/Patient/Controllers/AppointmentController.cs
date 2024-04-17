using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.viewmodels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]

    [Authorize(Roles = "Patient, Dentist")]
    public class AppointmentController : Controller
    {
        private IAppointment _appointment;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(IAppointment Appointment, UserManager<ApplicationUser> userManager)
        {
            _appointment = Appointment;
            _userManager = userManager;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            // Retrieve the current user's ID
            var userId = _userManager.GetUserId(User);

            // Get appointments only for the current user
            var appointments = _appointment.GetAllForUser(userId, pageNumber, pageSize);

            return View(appointments);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Clinic = new SelectList(_clinic.GetAll(), "Id", "Name");
            var viewModel = _appointment.GetById(id);
            return View(viewModel);
        }
      
        [HttpGet]
        public IActionResult Create()
        {
            // Get list of dentists
            var dentists = _userManager.Users.Where(u => u.IsDentist).ToList();

            // Map dentist list to SelectListItem for dropdown
            var dentistList = dentists.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id
            }).ToList();

            ViewBag.DentistList = dentistList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel vm)
        {
            // Get the currently logged-in user's ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user's ID is null or empty
            if (string.IsNullOrEmpty(userId))
            {
                // If the user's ID is null or empty, return a BadRequest response
                return BadRequest("User ID not found.");
            }

            // Set the user's ID as the PatientId in the view model
            vm.PatientId = userId;

            // Insert the appointment
            _appointment.InsertAppointment(vm);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _appointment.DeleteAppointment(id);
            return RedirectToAction("Index");
        }
















        /*private readonly ApplicationDbContext _context;
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
        */
    }
}
