using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BDAnimalsApi.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Kingdom { get; set; }
        [Required]

        public string Phylum { get; set; }
        [Required]

        public string Order { get; set; }
        [Required]

        public string Family { get; set; }
        [Required]

        public string Genus { get; set; }
        [Required]

        public string ScientificName { get; set; }
        [Required]

        public int ScientificClassId { get; set;}
        [Required]

        [ForeignKey("ScientificClassId")]
        public ScientificClass ScientificClass { get; set; }

        public byte[] Picture { get; set; }


    }
}
