using Microsoft.AspNetCore.Mvc;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
