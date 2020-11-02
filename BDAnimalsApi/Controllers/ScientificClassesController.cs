using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BDAnimalsApi.Models;
using BDAnimalsApi.Models.Dtos;
using BDAnimalsApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BDAnimalsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScientificClassesController : ControllerBase
    {
        private readonly IScientificClassRepository _scientificClassRepository;
        private readonly IMapper _mapper;
        public ScientificClassesController(IScientificClassRepository scientificClassRepository,
                                           IMapper mapper)
        {
            _scientificClassRepository = scientificClassRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get List Scientific Class.
        /// </summary>
        /// <returns></returns> 

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ScientificClassDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetScientificClasses()
        {
            var objList = _scientificClassRepository.GetScientificClasses();
            var objDto = new List<ScientificClassDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<ScientificClassDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get Individual Scientific Class.
        /// </summary>
        /// <param name="scientificClassId">The Id of Scientific Class</param>
        /// <returns></returns>

        [HttpGet("{scientificClassId:int}", Name = "GetScientificClass")]
        [ProducesResponseType(200, Type = typeof(ScientificClassDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetScientificClass(int scientificClassId)
        {
            var obj = _scientificClassRepository.GetScientificClass(scientificClassId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ScientificClassDto>(obj);
            return Ok(objDto);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ScientificClassDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatescientificClass([FromBody] ScientificClassDto scientificClassDto)
        {
            if (scientificClassDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_scientificClassRepository.ScientificClassExits(scientificClassDto.Name))
            {
                ModelState.AddModelError("", "Scientific Class Exits!");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var scientificClassObj = _mapper.Map<ScientificClass>(scientificClassDto);
            if (!_scientificClassRepository.CrateScientificClass(scientificClassObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when saving the record {scientificClassObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetScientificClass", new
            {
                scientificClassId = scientificClassObj.Id
            }, scientificClassObj);

        }

        [HttpPatch("{scientificClassId:int}", Name = "UpadatescientificClass")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ScientificClassDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScientificClass(int scientificClassId, [FromBody] ScientificClassDto scientificClassDto)
        {
            if (scientificClassDto == null || scientificClassId != scientificClassDto.Id)
            {
                return BadRequest(ModelState);
            }

            var natiotionalParkObj = _mapper.Map<ScientificClass>(scientificClassDto);
            if (!_scientificClassRepository.UpdateScientificClass(natiotionalParkObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when updation the record {natiotionalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{scientificClassId:int}", Name = "DeletescientificClass")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteScientificClass(int scientificClassId)
        {

            if (!_scientificClassRepository.ScientificClassExits(scientificClassId))
            {
                return NotFound();
            }

            var scientificClassObj = _scientificClassRepository.GetScientificClass(scientificClassId);
            if (!_scientificClassRepository.DeleteScientificClass(scientificClassObj))
            {
                ModelState.AddModelError("", $"Somethings went wrong when updation the record {scientificClassObj.Name}");
                return StatusCode(500, ModelState);

            }
            return NoContent();
        }
    }
}
