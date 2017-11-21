using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.Models;
using AnagramSolver.Utility;

namespace AnagramSolver.Services
{
    class TreeMultiWordSolver : IAnagramSolver
    {
        bool LimitToSingleWord { get; set; }

        private TreeParser Tree { get; set; }
        public TreeMultiWordSolver(string filePath)
        {
            LimitToSingleWord = false;

            Tree = new TreeParser();
            Tree.Parse(filePath);

            //Mark "a" and "i" as words
            Tree.Root('I').IsWord = true;
            Tree.Root('A').IsWord = true;
        }

        public List<string> Anagrams(string word, AnagramDictionary dictionary)
        {
            var results = new List<string>();

            var upper = word.Replace(" ", string.Empty).ToUpper();

            //Recusively start searching the tree for anagrams for each possible starting char
            results.AddRange(StartScan(upper));

            return results;
        }

        public List<string> StartScan(string chars)
        {
            List<string> results = new List<string>();

            Dictionary<char, bool> scanned = new Dictionary<char, bool>();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (scanned.ContainsKey(c))
                    continue;

                scanned.Add(c, true);
                var root = Tree.Root(c);
                results.AddRange(ScanTree(root, chars.Remove(i, 1)));
            }

            return results;
        }

        public List<string> ScanTree(TreeNode node, string chars)
        {
            List<string> results = new List<string>();
            if(node.IsWord)
            {
                string word = node.Word();
                if(string.IsNullOrEmpty(chars))
                {
                    results.Add(word);
                }
                else if(!LimitToSingleWord)
                {
                    //Scan remaining 
                    var subResults = StartScan(chars);
                    if(subResults.Count > 0)
                    {
                        results.AddRange(subResults.Select(x => string.Format("{0} {1}", word, x)));
                    } 
                }
            }

            if (chars.Length > 0)
            {
                Dictionary<char, bool> scanned = new Dictionary<char, bool>();
                //For each child in node that is in chars, recurse
                for (int i=0; i<chars.Length; i++)
                {
                    char c = chars[i];

                    if (scanned.ContainsKey(c))
                        continue;

                    scanned.Add(c, true);
                    var child = node.GetChild(c);
                    if(child!= null)
                    {
                        results.AddRange(ScanTree(child, chars.Remove(i, 1)));
                    }
                }
            }

            return results;
        }
    }
}