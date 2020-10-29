using BDAnimals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimals.Repository.IRepository
{
    public interface IScientificClassRepository
    {
        ICollection<ScientificClass> GetScientificClasses();
        ScientificClass GetScientificClass(int scientificClassId);
        bool ScientificClassExits(string name);
        bool ScientificClassExits(int id);

        bool CrateScientificClass(ScientificClass scientificClass);
        bool UpdateScientificClass(ScientificClass scientificClass);
        bool DeleteScientificClass(ScientificClass scientificClass);

        bool Save();
    }
}
