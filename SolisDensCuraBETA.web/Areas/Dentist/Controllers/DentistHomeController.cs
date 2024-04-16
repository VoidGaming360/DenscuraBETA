using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.web.Models;
using System.Diagnostics;

namespace SolisDensCuraBETA.web.Controllers
{
    [Area("Dentist")]

    
    public class DentistHomeController : Controller
    {
        private readonly ILogger<DentistHomeController> _logger;

        public DentistHomeController(ILogger<DentistHomeController> logger)
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
