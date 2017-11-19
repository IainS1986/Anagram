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
        private readonly IDictionary<string, List<string>> m_anagrams;

        public MultiWordSolver()
        {
            m_anagrams = new Dictionary<string, List<string>>();

            //Our dictionary is scrabble words only, for the purpose of this solver
            //I would like sentences to be possible, so I'm manually adding in I and A
            m_anagrams.Add("A", new List<string>() { "A" });
            m_anagrams.Add("I", new List<string>() { "I" });
        }

        public List<string> Anagrams(string word, AnagramDictionary dictionary)
        {
            var results = new List<string>();

            var upper = word.Replace(" ", string.Empty).ToUpper();

            //First, this could be expensive but for now, generate ALL permutations of this sorted string
            //Then, for each string, take an ever increasing PREFIX, looking for anagrams, then repeat on the remaining SUFFIX...
            //Build a running cache as we *will* be repeating the same lookups multiple times
            var permutations = upper.Permutations();
            foreach (var v in permutations)
            {
                results.AddRange(RecursiveAnagram(string.Empty, v, dictionary));
            }

            //Sort multi word anagrams into alphabetical order so we can remove duplicates
            for(int i=0; i<results.Count; i++)
            {
                string[] splits = results[i].Split(' ');
                Array.Sort(splits);
                results[i] = string.Join(" ", splits);
            }

            results = results.Distinct().ToList();

            return results;
        }

        public List<string> RecursiveAnagram(string prefix, string suffix, AnagramDictionary dictionary)
        {
            var result = new List<string>();

            //If Prefix is anagramable
            //Then generate every permutaiton of suffix
            //Run on suffix and combine result with results from prefix
            if(!string.IsNullOrEmpty(prefix))
            {
                //Look for anagrams of prefix
                var prefixAnagrams = FindAnagramsForString(prefix, dictionary);

                if(prefixAnagrams.Any())
                {
                    if(string.IsNullOrEmpty(suffix))
                    {
                        result.AddRange(prefixAnagrams);
                    }
                    else
                    {
                        //Run recursively on sub string and combine each result with prefix anagram
                        var suffixAnagrams = new List<string>();
                        if (m_anagrams.ContainsKey(suffix))
                            suffixAnagrams = m_anagrams[suffix];
                        else
                            suffixAnagrams = RecursiveAnagram(string.Empty, suffix, dictionary);

                        result.AddRange(prefixAnagrams.Zip(suffixAnagrams, (p, s) => string.Format("{0} {1}", p, s)));
                    }
                }
            }

            //Move 1 letter from Suffix to Prefix
            if(suffix.Length > 0)
            {
                prefix += suffix[0];
                suffix = suffix.Substring(1);
                //Recall
                result.AddRange(RecursiveAnagram(prefix, suffix, dictionary));
            } 

            return result;

        }

        private List<string> FindAnagramsForString(string str, AnagramDictionary dictionary)
        {
            var key = str.Alphabetically();

            //First look to see if we have already done this
            if(m_anagrams.ContainsKey(key))
            {
                return m_anagrams[key];
            }
            else
            {
                var anagrams = new List<string>();
                if(dictionary.Values.ContainsKey(key))
                {
                    anagrams.AddRange(dictionary.Values[key]);
                }

                //Cache before we return so we never do this again...
                m_anagrams.Add(key, anagrams);
                return anagrams;
            }
        }
    }
}
