using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BDAnimalsWeb.Models;
using BDAnimalsWeb.Repository.IRepository;

namespace BDAnimalsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimalRepository _animalRepository;

        public HomeController(ILogger<HomeController> logger, IAnimalRepository animalRepository)
        {
            _logger = logger;
            _animalRepository = animalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _animalRepository.GetAllAsync(StaticNames.AnimalAPIPath);
            return View(data);
        }
		
		public async Task<IActionResult> GetAnimal(int id)
        {
            if(id !=0)
            {
                var data = await _animalRepository.GetAsync(StaticNames.AnimalAPIPath, id);

                if(data !=null)
                {
                    return View(data);
                }
                return NotFound();
            }
            return NotFound();
        }


        public IActionResult Privacy()
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
