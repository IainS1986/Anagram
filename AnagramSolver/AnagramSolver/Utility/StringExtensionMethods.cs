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
    }
}
