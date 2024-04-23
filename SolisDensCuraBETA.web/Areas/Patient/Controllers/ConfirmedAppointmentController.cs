using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles ="Dentist")]
    public class ConfirmedAppointmentController : Controller
    {
        private readonly IAppointment _appointmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmedAppointmentController(IAppointment appointmentService, UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Retrieve the current logged-in user's ID (dentist)
            var dentistId = _userManager.GetUserId(User);

            // Retrieve confirmed appointments for the current dentist
            var confirmedAppointments = _appointmentService.GetConfirmedAppointments(dentistId);

            return View(confirmedAppointments);
        }

        [HttpPost]
        public IActionResult Proceed(int appointmentId)
        {
            // Redirect to the Create action of the TreatmentController with the appointmentId parameter
            return RedirectToAction("Create", "Treatment", new { appointmentId });
        }
    }
}
