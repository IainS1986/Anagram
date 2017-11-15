using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Models;
using AnagramSolver.Utility;

namespace AnagramSolver.Services
{
    class MultiWordSolver : IAnagramSolver
    {
        public List<string> Anagrams(string word, AnagramDictionary dictionary)
        {
            var upper = word.ToUpper();
            var sorted = upper.Alphabetically();

            //Recursively divide and conquer the string building up

            throw new NotImplementedException();
        }
    }
}
