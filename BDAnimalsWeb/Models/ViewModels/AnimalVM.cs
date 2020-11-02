using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimalsWeb.Models.ViewModels
{
    public class AnimalVM
    {
        public IEnumerable<SelectListItem> ScientificClass { get; set; }
        public Animal Animal { get; set; }
    }
}
