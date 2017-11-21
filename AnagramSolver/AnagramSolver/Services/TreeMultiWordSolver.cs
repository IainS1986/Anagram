﻿using System;
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
        private TreeParser Tree { get; set; }
        public TreeMultiWordSolver(string filePath)
        {
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
            for (int i=0; i<upper.Length; i++)
            {
                var root = Tree.Root(upper[i]);
                results.AddRange(ScanTree(root, upper.Remove(i, 1)));
            }

            return results.Distinct().ToList();
        }

        public List<string> ScanTree(TreeNode node, string chars)
        {
            List<string> results = new List<string>();
            if (node.IsWord && string.IsNullOrEmpty(chars))
            {
                string word = node.Word();
                results.Add(word);
            }
            else if(chars.Length > 0)
            {
                //For each child in node that is in chars, recurse
                for(int i=0; i<chars.Length; i++)
                {
                    var child = node.GetChild(chars[i]);
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