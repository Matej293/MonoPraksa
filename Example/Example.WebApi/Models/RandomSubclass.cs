using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example.WebApi.Models
{
    public class RandomSubclass
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string RandomArg1 { get; set; }
        [Required]
        public int RandomArg2 { get; set; }
    }
}