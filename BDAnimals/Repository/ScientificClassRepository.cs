using BDAnimals.Data;
using BDAnimals.Models;
using BDAnimals.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimals.Repository
{
    public class ScientificClassRepository : IScientificClassRepository
    {
        private readonly ApplicationDbContext _context;
        public ScientificClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CrateScientificClass(ScientificClass scientificClass)
        {
            _context.ScientificClass.Add(scientificClass);
            return Save();
        }

        public bool DeleteScientificClass(ScientificClass scientificClass)
        {
            _context.ScientificClass.Remove(scientificClass);
            return Save();
        }

        public ScientificClass GetScientificClass(int scientificClassId)
        {
            return _context.ScientificClass.FirstOrDefault(s => s.Id == scientificClassId);
        }

        public ICollection<ScientificClass> GetScientificClasses()
        {
            return _context.ScientificClass.OrderBy(s => s.Name).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool ScientificClassExits(string name)
        {
            bool value = _context.ScientificClass.Any(s=>s.Name.ToLower().Trim()==name.ToLower().Trim());
            return value;
        }

        public bool ScientificClassExits(int id)
        {
            bool value = _context.ScientificClass.Any(s => s.Id==id);
            return value;
        }

        public bool UpdateScientificClass(ScientificClass scientificClass)
        {
            _context.ScientificClass.Update(scientificClass);
            return Save();
        }
    }
}
