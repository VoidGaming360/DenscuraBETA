using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.model;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Dentist")] // Restrict access to dentists only
    public class CostController : Controller
    {
        private readonly ICostService _costService;

        public CostController(ICostService costService)
        {
            _costService = costService;
        }

        // Action to display invoices for existing treatments
        public IActionResult Index()
        {
            var costs = _costService.GetCostsForExistingTreatments();
            return View(costs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Include anti-forgery token to prevent CSRF attacks
        [Authorize(Roles = "Dentist")] // Ensure only dentists can access this action
        public async Task<IActionResult> UpdatePaymentStatus(int treatmentId, PaymentStatus paymentStatus)
        {
            // Implement logic to update the payment status here
            // Ensure proper authorization and validation before updating

            // Redirect back to the invoice index page after updating
            return RedirectToAction(nameof(Index));
        }
    }
}
