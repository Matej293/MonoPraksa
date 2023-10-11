using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Model.Common
{
    public interface ICityModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Country { get; set; }
        int Population { get; set; }
        Guid RandomSubclassId { get; set; }
        IRandomSubclassModel RandomSubclass { get; set; }
    }
}
