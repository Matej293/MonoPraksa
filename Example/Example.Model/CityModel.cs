using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Model.Common;

namespace Example.Model
{
    public class CityModel : ICityModel
    {    
        public Guid Id { get; set; } 
        public string Name { get; set;} 
        public string Country { get; set;} 
        public int Population { get; set; }
        public Guid RandomSubclassId { get; set; }
        public RandomSubclassModel RandomSubclass { get; set; }
    }
}
