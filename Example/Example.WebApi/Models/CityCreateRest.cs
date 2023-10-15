using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Example.Model.Common;

namespace Example.WebApi.Models
{
    public class CityCreateRest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public Guid RandomSubclassId { get; set; }
        public IRandomSubclassModel RandomSubclass { get; set; }
    }
}