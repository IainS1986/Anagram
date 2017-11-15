using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Models
{
    public class AnagramDictionary
    {
        public Dictionary<string, List<string>> Values
        {
            get;
            private set;
        }

        public AnagramDictionary()
        {
            Values = new Dictionary<string, List<string>>();
        }
    }
}
