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
using SolisDensCuraBETA.repositories.Interfaces;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]

    [Authorize(Roles = "Patient, Dentist")]
    public class AppointmentController : Controller
    {
        private IAppointment _appointment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITreatmentService _treatmentService;

        public AppointmentController(IAppointment Appointment, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _appointment = Appointment;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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

        [HttpGet]
        public IActionResult ViewAppointments()
        {
            // Retrieve the current logged-in user's ID
            var currentUserId = _userManager.GetUserId(User);

            // Retrieve appointments for the current user (dentist)
            var appointments = _appointment.GetAppointmentsForDentist(currentUserId);

            // Pass the appointments to the view
            return View(appointments);
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

        [HttpPost]
        [Authorize(Roles = "Dentist")] // Restrict access to dentists only
        public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, string status)
        {
            // Retrieve the current logged-in user's ID (dentist)
            var currentUserId = _userManager.GetUserId(User);

            // Retrieve appointments for the current user (dentist)
            var appointments = _appointment.GetAppointmentsForDentist(currentUserId);

            // Check if the appointment with the provided appointmentId exists in the retrieved appointments
            var appointment = appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appointment != null)
            {
                // Update the status of the appointment
                appointment.AppointmentStatus = status;
                _appointment.UpdateAppointment(appointment);
            }
            if (status.ToLower() == "confirmed")
            {
                return PartialView("_SetAppointmentDate", new SetAppointmentDateViewModel { AppointmentId = appointmentId });
            }
            // Redirect to the same page or a different page
            return RedirectToAction("ViewAppointments");
        }

        [HttpPost]
        public IActionResult SetAppointmentDate(SetAppointmentDateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return error response
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
            }

            try
            {
                // Retrieve the appointment by ID from the database
                var appointment = _unitOfWork.GenericRepositories<Appointment>().GetById(model.AppointmentId);

                // Check if appointment exists
                if (appointment == null)
                {
                    return Json(new { success = false, message = "Appointment not found" });
                }

                // Check if the appointment status is confirmed
                if (appointment.AppointmentStatus != AppointmentStatus.confirmed.ToString())
                {
                    return Json(new { success = false, message = "Appointment status must be confirmed to set appointment date" });
                }

                // Update the appointment date
                appointment.AppointmentDate = model.AppointmentDate;

                // Update appointment in the database
                _unitOfWork.GenericRepositories<Appointment>().Update(appointment);
                _unitOfWork.Save();

                // Determine the response message based on the appointment status
                string responseMessage = "Appointment date set successfully";
                if (appointment.AppointmentStatus == AppointmentStatus.pending.ToString())
                {
                    responseMessage = "Wait for confirmation";
                }
                else if (appointment.AppointmentStatus == AppointmentStatus.denied.ToString())
                {
                    responseMessage = "Appointment Denied";
                }

                // Redirect to the "ViewAppointments" page
                return RedirectToAction("ViewAppointments");
                //return Json(new { success = true, message = responseMessage });
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { success = false, message = "An error occurred while setting the appointment date: " + ex.Message });
            }
        }

        public IActionResult Delete(int id)
        {
            _appointment.DeleteAppointment(id);
            return RedirectToAction("Index");
        }


    }
}
