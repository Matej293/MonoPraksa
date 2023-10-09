using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Model
{
    public class CityModel
    {    
        public int Id { get; set; } 
        public string Name { get; set;} 
        public string Country { get; set;} 
        public int Population { get; set; }
        public int RandomSubclassId { get; set; }
        public RandomSubclassModel RandomSubclassModel { get; set; } // object used in joining
    }
}
