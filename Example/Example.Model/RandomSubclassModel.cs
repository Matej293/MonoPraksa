using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Model.Common;

namespace Example.Model
{
    public class RandomSubclassModel : Common.RandomSubclassModel
    {
        public Guid Id { get; set; }
        public string RandomArg1 { get; set; }
        public int RandomArg2 { get; set; }
    }
}
