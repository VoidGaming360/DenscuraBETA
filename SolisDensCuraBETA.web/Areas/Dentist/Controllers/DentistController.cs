using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SolisDensCuraBETA.web.Areas.Dentist.Controllers
{
    [Area("Dentist")]

    [Authorize(Roles = "Admin, Dentist")]

    public class DentistController : Controller
    {
        private DentistService _dentistService;

        public DentistController(DentistService dentistService)
        {
            _dentistService = dentistService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            // Fetch the desired page of data
            var pagedResult = _dentistService.GetAll(pageNumber, pageSize);

            // Extract the collection of TimingViewModel from the paged result
            var timingViewModels = pagedResult.Data; // Assuming Data property contains TimingViewModel collection

            return View(timingViewModels);
        }

        [HttpGet]
        public IActionResult AddTiming()
        {
            List<SelectListItem> morningShiftStart = new List<SelectListItem>();
            List<SelectListItem> morningShiftEnd = new List<SelectListItem>();
            List<SelectListItem> afternoonShiftStart = new List<SelectListItem>();
            List<SelectListItem> afternoonShiftEnd = new List<SelectListItem>();

            for (int i = 1; i <= 11; i++)
            {
                morningShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            for (int i = 1; i <= 13; i++)
            {
                morningShiftEnd.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            for (int i = 13; i <= 16; i++)
            {
                afternoonShiftStart.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            for (int i = 13; i <= 18; i++)
            {
                afternoonShiftEnd.Add(new SelectListItem { Text = (i + 1).ToString(), Value = i.ToString() });
            }

            ViewBag.morningStart = new SelectList(morningShiftStart, "Value", "Text");
            ViewBag.morningEnd = new SelectList(morningShiftEnd, "Value", "Text");
            ViewBag.afternoonStart = new SelectList(afternoonShiftStart, "Value", "Text");
            ViewBag.afternoonEnd = new SelectList(afternoonShiftEnd, "Value", "Text");

            TimingViewModel vm = new TimingViewModel();
            vm.Date = DateTime.Now;
            vm.Date = vm.Date.AddDays(1);

            ViewData["Status"] = Enum.GetValues(typeof(Status))
                             .Cast<Status>()
                             .Select(v => new SelectListItem
                             {
                                 Text = v.ToString(),
                                 Value = ((int)v).ToString()
                             });

            return View();
        }

        [HttpPost]
        public IActionResult AddTiming(TimingViewModel vm)
        {
            var ClaimsIdentity = (ClaimsIdentity)User.Identity;
            var Claims = ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (Claims != null)
            {
                vm.DoctorID.Id = Claims.Value;
                _dentistService.AddTiming(vm);
            }

            return RedirectToAction("Index");
        }

    }
}
