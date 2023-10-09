using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example.WebApi.Models
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int Population { get; set; }
        public int RandomSubclassId { get; set; }
        public RandomSubclass RandomSubclass { get; set; } // object used in joining
    }
}