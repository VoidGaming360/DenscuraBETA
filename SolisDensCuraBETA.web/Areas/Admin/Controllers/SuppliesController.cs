using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using SolisDensCuraBETA.viewmodels;
using Microsoft.AspNetCore.Components;
using SolisDensCuraBETA.services.Interface;

namespace SolisDensCuraBETA.web.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class SuppliesController : Controller
    {
        private ISupplies _supply;

        public SuppliesController(ISupplies Supply)
        {
            _supply = Supply;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_supply.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Clinic = new SelectList(_clinic.GetAll(), "Id", "Name");
            var viewModel = _supply.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(SuppliesViewModel vm)
        {
            _supply.UpdateSupplies(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SuppliesViewModel vm)
        {
            _supply.InsertSupplies(vm);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _supply.DeleteSupplies(id);
            return RedirectToAction("Index");
        }
    }
}
