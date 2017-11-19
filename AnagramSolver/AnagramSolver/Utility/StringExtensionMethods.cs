using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Utility
{
    static class StringExtensionMethods
    {
        public static string Alphabetically(this string str)
        {
            //Rearrange the word chars alphabetically
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            return string.Concat(chars);
        }

        public static List<string> Permutations(this string str)
        {
            return Permutation(string.Empty, str);
        }

        private static List<string> Permutation(string prefix, string suffix)
        {
            var results = new List<string>();
            if(suffix.Length == 0)
            {
                results.Add(prefix);
            }
            else
            {
                for(int i=0; i<suffix.Length; i++)
                {
                    results.AddRange(Permutation(prefix + suffix[i], suffix.Substring(0, i) + suffix.Substring(i + 1)));
                }
            }
            return results;
        }
    }
}
