using AnagramSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Services
{
    interface IDictionaryParser
    {
        AnagramDictionary Parse(string filename);
    }
}
