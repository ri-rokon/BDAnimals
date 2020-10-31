using BDAnimals.Data;
using BDAnimals.Models;
using BDAnimals.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimals.Repository
{
    public class AnimalRepository: IAnimalRepository
    {
        private readonly ApplicationDbContext _context;
        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool CrateAnimal(Animal animal)
        {
            _context.Animal.Add(animal);
            return Save();
        }

        public bool DeleteAnimal(Animal animal)
        {
            _context.Animal.Remove(animal);
            return Save();
        }

        public Animal GetAnimal(int animalId)
        {
            return _context.Animal.Include(c => c.ScientificClass).FirstOrDefault(c => c.Id == animalId);
        }

        public ICollection<Animal> GetAnimals()
        {
            return _context.Animal.Include(c=>c.ScientificClass).OrderBy(s => s.Name).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool AnimalExits(string name)
        {
            bool value = _context.Animal.Any(s => s.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool AnimalExits(int id)
        {
            bool value = _context.Animal.Any(s => s.Id == id);
            return value;
        }

        public bool UpdateAnimal(Animal animal)
        {
            _context.Animal.Update(animal);
            return Save();
        }

        public ICollection<Animal> GetAnimalsInScientificClass(int scientificClassId)
        {
            return _context.Animal.Include(c => c.ScientificClass).Where(c => c.ScientificClassId == scientificClassId).ToList();

        }
    }
}
