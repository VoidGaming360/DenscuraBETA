using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Dentist")]
    public class TreatmentController : Controller // Change ControllerBase to Controller
    {
        private readonly ITreatmentService _treatmentService;

        public TreatmentController(ITreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var treatments = _treatmentService.GetAllTreatments();
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                await _treatmentService.AddTreatmentAsync(treatment);
                return RedirectToAction(nameof(Index));
            }
            return View(treatment);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            return View(treatment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Treatment treatment)
        {
            if (id != treatment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _treatmentService.UpdateTreatmentAsync(treatment);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(treatment);
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

    }
}
