using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.services.Interface;
using SolisDensCuraBETA.viewmodels;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Dentist, Patient")]
    public class TreatmentController : Controller
    {
        private readonly IAppointment _appointmentService;
        private readonly ITreatmentService _treatmentService;
        private readonly ICostService _costService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TreatmentController> _logger;

        public TreatmentController(
            IAppointment appointmentService,
            ITreatmentService treatmentService,
            ICostService costService,
            UserManager<ApplicationUser> userManager,
            ILogger<TreatmentController> logger)
        {
            _appointmentService = appointmentService;
            _treatmentService = treatmentService;
            _costService = costService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var userRoles = await _userManager.GetRolesAsync(await _userManager.GetUserAsync(User));

            IEnumerable<Treatment> treatments;

            if (userRoles.Contains("Patient"))
            {
                var patientAppointments = _appointmentService.GetAppointmentsForPatient(userId);
                var appointmentIds = patientAppointments.Select(a => a.Id);
                treatments = await _treatmentService.GetTreatmentsByAppointmentIdsAsync(appointmentIds);
            }
            else if (userRoles.Contains("Dentist"))
            {
                var dentistAppointments = _appointmentService.GetAppointmentsForDentist(userId);
                var appointmentIds = dentistAppointments.Select(a => a.Id);
                treatments = await _treatmentService.GetTreatmentsByAppointmentIdsAsync(appointmentIds);
            }
            else
            {
                treatments = Enumerable.Empty<Treatment>();
            }

            return View(treatments);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            return View(treatment);
        }

        [HttpGet]
        public IActionResult Create(int appointmentId)
        {
            var appointment = _appointmentService.GetById(appointmentId);
            if (appointment == null || appointment.AppointmentStatus != AppointmentStatus.confirmed.ToString())
            {
                return NotFound("Confirmed appointment not found");
            }

            var model = new TreatmentViewModel
            {
                AppointmentId = appointmentId,
                AppointmentDate = appointment.AppointmentDate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TreatmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var treatment = new Treatment
                {
                    AppointmentId = model.AppointmentId,
                    Number = model.Number,
                    Notes = model.Notes,
                    TreatmentPlan = model.TreatmentPlan,
                    Description = model.Description,
                    Prescription = model.Prescription,
                    Diagnosis = model.Diagnosis,
                    Costs = model.Costs
                };

                await _treatmentService.AddTreatmentAsync(treatment);
                return RedirectToAction("Index", "ConfirmedAppointment", new { area = "Patient" });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);
            if (treatment == null)
            {
                return NotFound("Treatment not found");
            }

            var model = new TreatmentViewModel
            {
                Id = treatment.Id,
                AppointmentId = treatment.AppointmentId,
                Number = treatment.Number,
                Notes = treatment.Notes,
                TreatmentPlan = treatment.TreatmentPlan,
                Description = treatment.Description,
                Prescription = treatment.Prescription,
                Diagnosis = treatment.Diagnosis,
                Costs = treatment.Costs
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TreatmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var treatment = new Treatment
                {
                    Id = model.Id,
                    AppointmentId = model.AppointmentId,
                    Number = model.Number,
                    Notes = model.Notes,
                    TreatmentPlan = model.TreatmentPlan,
                    Description = model.Description,
                    Prescription = model.Prescription,
                    Diagnosis = model.Diagnosis,
                    Costs = model.Costs
                };

                await _treatmentService.UpdateTreatmentAsync(treatment);
                return RedirectToAction("Index", "Treatment", new { area = "Patient" });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            return View(treatment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _treatmentService.DeleteTreatmentAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Pay(int treatmentId)
        {
            // Log the treatmentId received
            _logger.LogInformation("Received treatmentId: {TreatmentId}", treatmentId);

            var treatment = await _treatmentService.GetTreatmentByIdAsync(treatmentId);
            if (treatment == null)
            {
                _logger.LogWarning("Treatment not found for treatmentId: {TreatmentId}", treatmentId);
                return NotFound("Treatment not found");
            }

            if (treatment.IsPaid == null)
            {
                treatment.IsPaid = false;
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // Generate invoice
            var cost = new Cost
            {
                TreatmentId = treatment.Id,
                Treatment = treatment,
                TotalCost = treatment.Costs,
                CustomerName = user.UserName,
                Date = DateTime.Now,
                PaymentStatus = PaymentStatus.Pending
            };

            await _costService.AddCostAsync(cost);

            // Update treatment to indicate payment initiated
            treatment.IsPressed = true;
            await _treatmentService.UpdateTreatmentAsync(treatment);

            // Redirect to Invoice list view
            return RedirectToAction("InvoiceList", new { area = "Patient" });
        }


        [HttpGet]
        [Authorize(Roles = "Patient, Dentist")]
        public async Task<IActionResult> InvoiceList()
        {
            var userId = _userManager.GetUserId(User);
            var userRoles = await _userManager.GetRolesAsync(await _userManager.GetUserAsync(User));

            IEnumerable<Cost> costs;

            if (userRoles.Contains("Patient"))
            {
                var patientAppointments = _appointmentService.GetAppointmentsForPatient(userId);
                var appointmentIds = patientAppointments.Select(a => a.Id);
                var treatments = await _treatmentService.GetTreatmentsByAppointmentIdsAsync(appointmentIds);
                var treatmentIds = treatments.Select(t => t.Id);
                costs = await _costService.GetCostsByTreatmentIdsAsync(treatmentIds);
            }
            else if (userRoles.Contains("Dentist"))
            {
                var dentistAppointments = _appointmentService.GetAppointmentsForDentist(userId);
                var appointmentIds = dentistAppointments.Select(a => a.Id);
                var treatments = await _treatmentService.GetTreatmentsByAppointmentIdsAsync(appointmentIds);
                var treatmentIds = treatments.Select(t => t.Id);
                costs = await _costService.GetCostsByTreatmentIdsAsync(treatmentIds);
            }
            else
            {
                costs = Enumerable.Empty<Cost>();
            }

            var costViewModels = costs.Select(cost => new CostViewModel
            {
                Id = cost.Id,
                TreatmentId = cost.TreatmentId,
                TotalCost = cost.TotalCost,
                CustomerName = cost.CustomerName,
                Date = cost.Date,
                PaymentStatus = cost.PaymentStatus.ToString()
            }).ToList();

            return View(costViewModels);
        }


        [HttpGet]
        [Authorize(Roles = "Patient, Dentist")]
        public async Task<IActionResult> Invoice(int costId)
        {
            var cost = await _costService.GetCostByIdAsync(costId);
            if (cost == null)
            {
                return NotFound("Invoice not found");
            }

            var costViewModel = new CostViewModel
            {
                Id = cost.Id,
                TreatmentId = cost.TreatmentId,
                TotalCost = cost.TotalCost,
                CustomerName = cost.CustomerName,
                Date = cost.Date,
                PaymentStatus = cost.PaymentStatus.ToString()
            };

            return View(costViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Dentist")]
        public async Task<IActionResult> UpdatePaymentStatus(int costId, string status)
        {
            var cost = await _costService.GetCostByIdAsync(costId);
            if (cost == null)
            {
                return NotFound("Invoice not found");
            }

            if (status == "Paid")
            {
                cost.PaymentStatus = PaymentStatus.Paid;
                var treatment = await _treatmentService.GetTreatmentByIdAsync(cost.TreatmentId);
                if (treatment != null)
                {
                    treatment.IsPaid = true;
                    treatment.IsPressed = true;
                    await _treatmentService.UpdateTreatmentAsync(treatment);
                }
            }

            await _costService.UpdateCostAsync(cost);

            return RedirectToAction("Index", "ConfirmedAppointment", new { area = "Patient" });
        }


    }
}
