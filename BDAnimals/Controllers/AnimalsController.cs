
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BDAnimals.Models;
using BDAnimals.Models.Dtos;
using BDAnimals.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BDAnimals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository ;
        private readonly IMapper _mapper;
        public AnimalsController(IAnimalRepository animalRepository,
                                           IMapper mapper)
        {
            _animalRepository = animalRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List Of Animal.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AnimalDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetAnimals()
        {
            var objList = _animalRepository.GetAnimals();
            var objDto = new List<AnimalDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<AnimalDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual Animal.
        /// </summary>
        /// <param name="animalId">The Id of Animal</param>
        /// <returns></returns>

        [HttpGet("{animalId:int}", Name = "GetAnimal")]
        [ProducesResponseType(200, Type = typeof(AnimalDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetAnimal(int animalId)
        {
            var obj = _animalRepository.GetAnimal(animalId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<AnimalDto>(obj);
            return Ok(objDto);
        }


        [HttpGet("GetAnimalInScientificClass/{scientificClassId:int}", Name = "GetAnimalInScientificClass")]
        [ProducesResponseType(200, Type = typeof(AnimalDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetAnimalsInScientificClass(int scientificClassId)
        {
            var objList = _animalRepository.GetAnimalsInScientificClass(scientificClassId);
            var objDtos = new List<AnimalDto>();
            if (objList == null)
            {
                return NotFound();
            }
            foreach (var obj in objList)
            {
                objDtos.Add(_mapper.Map<AnimalDto>(obj));
            }
            return Ok(objDtos);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AnimalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateAnimal([FromBody] AnimalCreateDto animalDto)
        {
            if (animalDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_animalRepository.AnimalExits(animalDto.Name))
            {
                ModelState.AddModelError("", "Animal Exits!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var animalObj = _mapper.Map<Animal>(animalDto);
            if (!_animalRepository.CrateAnimal(animalObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when saving the record {animalObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAnimal", new { animalId = animalObj.Id }, animalObj);

        }

        [HttpPatch("{animalId:int}", Name = "UpadateAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AnimalDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAnimal(int animalId, [FromBody] AnimalUpdateDto animalDto)
        {
            if (animalDto == null || animalId != animalDto.Id)
            {
                return BadRequest(ModelState);
            }

            var natiotionalParkObj = _mapper.Map<Animal>(animalDto);
            if (!_animalRepository.UpdateAnimal(natiotionalParkObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when updation the record {natiotionalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{animalId:int}", Name = "DeleteAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAnimal(int animalId)
        {

            if (!_animalRepository.AnimalExits(animalId))
            {
                return NotFound();
            }

            var AnimalObj = _animalRepository.GetAnimal(animalId);
            if (!_animalRepository.DeleteAnimal(AnimalObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when updation the record {AnimalObj.Name}");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
    }
}
