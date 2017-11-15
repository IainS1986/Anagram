using AnagramSolver.Models;
using AnagramSolver.Services;
using AnagramSolver.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver
{
    class Program
    {
        private const string cFilePath = "Dictionaries/dictionary.txt";

        private static IDictionaryParser Parser { get; set; }

        private static IAnagramSolver Solver { get; set; }

        private static AnagramDictionary Anagrams { get; set; }

        static void Main(string[] args)
        {
            Parser = new DictionaryParser();
            Solver = new SingleWordSolver();

            Anagrams = Parser.Parse(cFilePath);

            Console.WriteLine("Anagram Parser Ready!");

            while(true)
            {
                RunAnagramInput();
            }
        }

        private static void RunAnagramInput()
        {
            Console.WriteLine("Type in a word below and see if it has any anagrams...");
            var input = Console.ReadLine();

            List<string> anagrams = Solver.Anagrams(input, Anagrams);
            if (anagrams.Count > 0)
            {
                Console.WriteLine(string.Format("Found {0} anagram{1}!", anagrams.Count, (anagrams.Count > 1) ? "s" : string.Empty));
                for (int i = 0; i < anagrams.Count; i++)
                    Console.WriteLine("         {0}", anagrams[i]);
            }
            else
            {
                Console.WriteLine(string.Format("No Anagrams were found for {0}", input));
            }

            Console.WriteLine();
        }
    }
}
