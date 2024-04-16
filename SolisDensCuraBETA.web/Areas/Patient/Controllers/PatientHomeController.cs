using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.web.Models;
using System.Diagnostics;

namespace SolisDensCuraBETA.web.Controllers
{
    [Area("Patient")]

    [Authorize(Roles = "Patient")]
    public class PatientHomeController : Controller
    {
        private readonly ILogger<PatientHomeController> _logger;

        public PatientHomeController(ILogger<PatientHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
