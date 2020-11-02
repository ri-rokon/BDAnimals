using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BDAnimalsWeb.Models;
using BDAnimalsWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BDAnimalsWeb.Controllers
{
    public class ScientificClassesController : Controller
    {
        private readonly IScientificClassRepository _scRepository;
        public ScientificClassesController(IScientificClassRepository scRepository)
        {
            _scRepository = scRepository;
        }

        public IActionResult Index()
        {
            return View(new ScientificClass() { });
        }

        public async Task<IActionResult> GetAllScientificClass()
        {
            var obj = new { data = await _scRepository.GetAllAsync(StaticNames.ScientificClassAPIPath) };
            return Json(obj);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ScientificClass obj = new ScientificClass();
            if (id == null)
            {
                //this will be true for create or insert
                return View(obj);
            }

            //this will be for update
            obj = await _scRepository.GetAsync(StaticNames.ScientificClassAPIPath, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ScientificClass obj)
        {
            if (ModelState.IsValid)
            {
                 
                if (obj.Id == 0)
                {
                    await _scRepository.CreateAsync(StaticNames.ScientificClassAPIPath, obj);
                }
                else
                {
                    await _scRepository.UpdateAsync(StaticNames.ScientificClassAPIPath + obj.Id, obj);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _scRepository.DeleteAsync(StaticNames.ScientificClassAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }

    }
}
