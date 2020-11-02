using AutoMapper;
using BDAnimals.Models;
using BDAnimals.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimals.Mappers
{
    public class BDAnimalsMappings:Profile
    {
        public BDAnimalsMappings()
        {
            // Mapping properties from ScientificClass to ScientificClassDto and vice versa  
            CreateMap<ScientificClass, ScientificClassDto>().ReverseMap();

            // Mapping properties from Animal to AnimalDto and vice versa  
            CreateMap<Animal, AnimalDto>().ReverseMap();

            CreateMap<Animal, AnimalCreateDto>().ReverseMap();

            CreateMap<Animal, AnimalUpdateDto>().ReverseMap();



        }
    }
}
