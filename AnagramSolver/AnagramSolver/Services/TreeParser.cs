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
    /// <summary>
    /// Experimental parser, parsed the dictionary into a tree structure.
    /// Root is Empty.String then from there are 26 children, one for each letter of the alphabet,
    /// then from there, each letter has a child IF there is a word in that sequence, the leaf nodes of the
    /// the branches are the word...
    /// 
    /// a = 65...z=90
    /// </summary>
    class TreeParser
    {
        const int CharOffset = 65;

        private TreeNode[] Roots;

        public TreeNode Root(char c)
        {
            return Roots[(int)(c - CharOffset)];
        }
        
        public void Parse(string filename)
        {
            AnagramDictionary result = new AnagramDictionary();

            //Get path to file
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);

            Console.WriteLine(string.Format("Tree Parsing {0}", filename));
            //Parse dictionary into raw list of strings
            string[] lines = File.ReadAllLines(path);

            //Setup roots for the word tree
            Roots = new TreeNode[26];
            for(int i=0; i<26; i++)
            {
                Roots[i] = new TreeNode((char)(i + CharOffset));
            }

            //Process
            Console.WriteLine("Loading Tree Dictionary...");
            for(int i=0; i<lines.Length; i++)
            {
                string word = lines[i];
                string upper = word.ToUpper();
                string sorted = upper.Alphabetically();

                //Add word to tree
                TreeNode node = Root(upper[0]);
                for(int j = 1; j<upper.Length; j++)
                {
                    char c = upper[j];
                    //Look for child in current node, and add if not there
                    node = node.GetOrAddChild(c);
                }

                //Mark final node as "word"
                node.IsWord = true;
            }

            Console.WriteLine("Done!");
        }
    }

    class TreeNode
    {
        public TreeNode Parent { get; private set; }
        public List<TreeNode> Children { get; set; }
        public char Char { get; private set; }
        
        public bool IsWord { get; set; }
        public string Word { get; private set; }

        public TreeNode(char c) : this(c, null)
        {
        }

        public TreeNode(char c, TreeNode parent)
        {
            Char = c;
            //Setup children for potential leafs
            Children = new List<TreeNode>();
            Parent = parent;
            Word = string.Format("{0}{1}", Parent?.Word, Char);
        }

        public TreeNode GetOrAddChild(char c)
        {
            TreeNode node = Children.FirstOrDefault(x => x.Char == c);
            if(node == null)
            {
                node = new TreeNode(c, this);
                Children.Add(node);
            }

            return node;
        }

        public TreeNode GetChild(char c)
        {
            return Children.FirstOrDefault(x => x.Char == c);
        }
    }
}
