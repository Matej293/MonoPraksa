using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;

namespace Example.Model.Common
{
    public interface IRandomSubclassModel
    {
        Guid Id { get; set; }
        string RandomArg1 { get; set; }
        int RandomArg2 { get; set; }
    }
}
