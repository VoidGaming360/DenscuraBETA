using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.viewmodels;
using SolisDensCuraBETA.repositories.Migrations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Dentist")]
    public class TreatmentController : Controller
    {
        private readonly ITreatmentService _treatmentService;
        private IAppointment _appointment;
        private readonly UserManager<ApplicationUser> _userManager;

        public TreatmentController(ITreatmentService treatmentService, IAppointment Appointment, UserManager<ApplicationUser> userManager)
        {
            _treatmentService = treatmentService;
            _appointment = Appointment;
            _userManager = userManager;
        }

        
        public IActionResult Index(int appointmentId)
        {
            // Retrieve treatments for the given appointmentId
            var treatments = _treatmentService.GetTreatmentsForAppointment(appointmentId);
            return View(treatments);
        }
        /*
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new TreatmentViewModel(); // Assuming you have a view model for treatment plan
            return View(viewModel);
        }
        */
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new TreatmentViewModel();
            var dentistId = _userManager.GetUserId(User);

            // Retrieve confirmed appointments for the current dentist
            var confirmedAppointments = _appointment.GetConfirmedAppointments(dentistId);

            // Assign confirmed appointments to the viewModel
            viewModel.Appointments = confirmedAppointments;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TreatmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the view with validation errors
                return View(viewModel);
            }

            // Check if AppointmentId is selected
            if (viewModel.AppointmentId == 0)
            {
                // If no appointment is selected, add a model error
                ModelState.AddModelError("AppointmentId", "Please select an appointment.");
                return View(viewModel);
            }

            // Assuming you have a method to save the treatment plan in your service
            _treatmentService.CreateTreatment(viewModel);

            return RedirectToAction("Index");
        }




        /*
        [HttpGet]
        public IActionResult Create(int appointmentId)
        {
            var treatmentViewModel = new TreatmentViewModel
            {
                AppointmentId = appointmentId
            };
            return View(treatmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TreatmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var treatment = TreatmentViewModel.ConvertViewModelToTreatment(vm);
            // Insert the appointment
            _treatmentService.CreateTreatment(vm);

            return RedirectToAction("Index");
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Create(TreatmentViewModel treatmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(treatmentViewModel);
            }

            // Convert view model to Treatment model
            var treatment = TreatmentViewModel.ConvertViewModelToTreatment(treatmentViewModel);

            // Save the treatment using the service
            await _treatmentService.CreateTreatmentAsync(treatment);

            // Redirect to a success page or another action
            return RedirectToAction("Index", "Home");
        }

        // Other actions for updating, deleting treatments, etc.

        */
    }
}
