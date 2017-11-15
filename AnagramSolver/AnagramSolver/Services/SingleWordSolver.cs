using AnagramSolver.Models;
using AnagramSolver.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Services
{
    class SingleWordSolver : IAnagramSolver
    {
        public List<string> Anagrams(string word, AnagramDictionary dictionary)
        {
            var upper = word.ToUpper();
            var sorted = upper.Alphabetically();

            List<string> anagrams = new List<string>();
            if (dictionary.Values.ContainsKey(sorted))
            {
                anagrams.AddRange(dictionary.Values[sorted]);

                //Remove our word!
                anagrams.Remove(upper);
            }

            return anagrams;
        }
    }
}
