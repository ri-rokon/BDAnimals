using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BDAnimalsWeb.Models;
using BDAnimalsWeb.Models.ViewModels;
using BDAnimalsWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BDAnimalsWeb.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IScientificClassRepository _scRepository;
        private readonly IAnimalRepository _animalRepository;
        public AnimalsController(IScientificClassRepository scRepository, IAnimalRepository animalRepository)
        {
            _scRepository = scRepository;
            _animalRepository = animalRepository;
        }

        public IActionResult Index()
        {
            return View(new ScientificClass() { });
        }

        public async Task<IActionResult> GetAllAnimal()
        {
            var obj = new { data = await _animalRepository.GetAllAsync(StaticNames.AnimalAPIPath) };
            return Json(obj);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
                IEnumerable<ScientificClass> scList = await _scRepository.GetAllAsync(StaticNames.ScientificClassAPIPath);
                AnimalVM animalVM = new AnimalVM()
                {
                    ScientificClass=scList.Select(i=>new SelectListItem { 
                        Text=i.Name,
                        Value=i.Id.ToString()
                    }),
                    Animal= new Animal()

                };
            if(id==null)
            {
                //this will be true for create or insert
                return View(animalVM);

            }

            //Flow will come here for update
            animalVM.Animal = await _animalRepository.GetAsync(StaticNames.AnimalAPIPath,id.GetValueOrDefault());
            if (animalVM.Animal==null)
            {
                return NotFound();
            }
            return View(animalVM);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AnimalVM obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Animal.Picture = p1;
                }
                else
                {
                    var objFromDb = await _animalRepository.GetAsync(StaticNames.AnimalAPIPath, obj.Animal.Id);
                    if (objFromDb != null)
                    {
                        if (objFromDb.Picture != null)
                        {
                            obj.Animal.Picture = objFromDb.Picture;

                        }
                    }
                    
                }


                if (obj.Animal.Id == 0)
                {
                    await _animalRepository.CreateAsync(StaticNames.AnimalAPIPath, obj.Animal);
                }
                else
                {
                    await _animalRepository.UpdateAsync(StaticNames.AnimalAPIPath + obj.Animal.Id, obj.Animal);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {

                IEnumerable<ScientificClass> scList = await _scRepository.GetAllAsync(StaticNames.ScientificClassAPIPath);
                AnimalVM animalVM = new AnimalVM()
                {
                    ScientificClass = scList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    Animal = obj.Animal
                };
                return View(animalVM);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _scRepository.DeleteAsync(StaticNames.AnimalAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }

    }
}
