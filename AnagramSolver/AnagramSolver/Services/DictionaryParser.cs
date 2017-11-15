using AnagramSolver.Models;
using AnagramSolver.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Services
{
    class DictionaryParser : IDictionaryParser
    {
        public AnagramDictionary Parse(string filename)
        {
            AnagramDictionary result = new AnagramDictionary();

            //Get path to file
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);

            Console.WriteLine(string.Format("Parsing {0}", filename));
            //Parse dictionary into raw list of strings
            string[] lines = File.ReadAllLines(path);

            //Process
            Console.WriteLine("Loading Dictionary...");
            float increment = 1.0f / lines.Length;
            for(int i=0; i<lines.Length; i++)
            {
                string word = lines[i];
                string upper = word.ToUpper();
                string sorted = upper.Alphabetically();

                //Add to dictionary
                if (result.Values.ContainsKey(sorted))
                    result.Values[sorted].Add(word);
                else
                    result.Values.Add(sorted, new List<string>() { word });
            }

            Console.WriteLine("Done!");

            return result;
        }
    }
}
