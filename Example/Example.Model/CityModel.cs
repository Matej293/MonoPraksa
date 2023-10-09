using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Model
{
    public class CityModel
    {    
        public Guid Id { get; set; } 
        public string Name { get; set;} 
        public string Country { get; set;} 
        public int Population { get; set; }
        public Guid RandomSubclassId { get; set; }
        public RandomSubclassModel RandomSubclassModel { get; set; }
    }
}
