using BDAnimalsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimalsApi.Repository.IRepository
{
     public interface IAnimalRepository
    {
        ICollection<Animal> GetAnimals();
        ICollection<Animal> GetAnimalsInScientificClass(int scientificClassId);

        Animal GetAnimal(int animalId);
        bool AnimalExits(string name);
        bool AnimalExits(int id);

        bool CrateAnimal(Animal animal);
        bool UpdateAnimal(Animal animal);
        bool DeleteAnimal(Animal animal);

        bool Save();
    }
}
