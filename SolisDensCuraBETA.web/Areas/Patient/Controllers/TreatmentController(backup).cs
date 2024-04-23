using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.viewmodels;

namespace SolisDensCuraBETA.web.Controllers
{
    [Authorize(Roles = "Dentist")]
    public class TreatmentController : Controller
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        public IActionResult Create(int appointmentId)
        {
            // Fetch the appointment details
            var appointment = _treatmentService.GetAppointmentById(appointmentId);

            // Pass the appointment ID to the view so it can be used to associate the treatment plan with the appointment
            ViewBag.AppointmentId = appointmentId;

            return View();
        }

        [HttpPost]
        public IActionResult Create(TreatmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the treatment plan using the treatment service
                _treatmentService.CreateTreatment(model);

                // Redirect to a suitable page, such as the appointment details page
                return RedirectToAction("Index", "Appointment");
            }
            // If model state is not valid, return to the create view with validation errors
            return View(model);
        }
    }
}
