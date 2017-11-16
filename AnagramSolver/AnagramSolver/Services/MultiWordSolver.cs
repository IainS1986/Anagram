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
            return RecursiveAnagram(string.Empty, sorted);
        }

        public List<string> RecursiveAnagram(string prefix, string suffix)
        {
            //Get All Anagrams of Prefix and all Anagrams of Suffix.
            //Return a "string" of all possible combinations

            //Then, foreach char in suffix, call this function on prefix+char , suffix-char
        }
    }
}
