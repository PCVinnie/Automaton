using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Automaton
{
    class RegexParser : Representation
    {
        private string data; 
        private int next; 

        public RegexParser(string pattern)
        {
            data = Preprocess(pattern);
            next = 0;

            ParseTree parseTree = Expression();
            if (Peek() != '\0')
            {
                Console.WriteLine("Parse error: unexpected char, got {0} at #{1}", Peek(), GetPos());
                Console.ReadKey();
                return;
            }
            Console.WriteLine(data);
            PrintTree(parseTree, 1);
            NFA nfa = NFA.TreeToNFA(parseTree);
            nfa.Show();
            Console.Write("\n\n");
            Console.ReadKey();
        }
            
        private char Peek() 
        { 
            return (next < data.Length) ? data[next] : '\0';
        } 

        private char Pop() 
        { 
            char current = Peek(); 
            if(next < data.Length) 
                ++next; 
            return current; 
        }

        private int GetPos() 
        { 
            return next; 
        } 

        private static void PrintTree(ParseTree node, int offset)
        { 
            if(node == null) 
                return; 

            for(int i = 0; i < offset; ++i) 
                Console.Write(" "); 

            switch(node.type) 
            { 
                case ParseTree.NodeType.CHAR:
                    Console.WriteLine(node.data); 
                    break; 
                case ParseTree.NodeType.ALTER: 
                    Console.WriteLine("|");
                    break; 
                case ParseTree.NodeType.CONCAT: 
                    Console.WriteLine("."); 
                    break; 
                case ParseTree.NodeType.QUESTION: 
                    Console.WriteLine("?"); 
                    break; 
                case ParseTree.NodeType.STAR:
                    Console.WriteLine("*");
                    break;
                case ParseTree.NodeType.PLUS:
                    Console.WriteLine("+"); 
                    break; 
            } 

            Console.Write(""); 
            
            PrintTree(node.left, offset + 4); 
            PrintTree(node.right, offset + 4); 
        }

        private ParseTree Character() 
        { 
            char data = Peek(); 
            if(char.IsLetterOrDigit(data) || data == '\0')
                return new ParseTree(ParseTree.NodeType.CHAR, this.Pop(), null, null); 
            else 
            { 
                Console.WriteLine("Parse error: expected alphanumeric, got {0} at #{1}", Peek(), GetPos());
                Console.ReadKey(); 
                Environment.Exit(1); 
                return null; 
            } 
        }
            
        private ParseTree Atom() 
        { 
            ParseTree atomNode; 
            if(Peek() == '(') 
            { 
                Pop(); 
                atomNode = Expression(); 
                if(Pop() != ')') 
                {
                    Console.WriteLine("Parse error: expected ')'");
                    Console.ReadKey();
                    Environment.Exit(1); 
                } 
            } 
            else 
                atomNode = Character(); 
            return atomNode; 
        }
            
        private ParseTree Repetition() 
        { 
            ParseTree atomNode = Atom(); 
            if(Peek() == '*') 
            { 
                Pop(); 
                ParseTree repNode = new ParseTree(ParseTree.NodeType.STAR, ' ', atomNode, null); //null
                return repNode;
            } 
            else if (Peek() == '?') 
            { 
                Pop(); 
                ParseTree repNode = new ParseTree(ParseTree.NodeType.QUESTION, ' ', atomNode, null);
                return repNode;
            }
            else if (Peek() == '+')
            {
                Pop();
                ParseTree repNode = new ParseTree(ParseTree.NodeType.PLUS, ' ', atomNode, null);
                return repNode;
            }
            else 
                return atomNode; 
        }
            
        private ParseTree Concat()
        { 
            ParseTree left = Repetition();
            if(Peek() == '.') 
            { 
                Pop(); 
                ParseTree right = Concat(); 
                ParseTree concatNode = new ParseTree(ParseTree.NodeType.CONCAT, ' ', left, right); //null
                return concatNode;
            } else 
                return left; 
        }

        private ParseTree Expression() 
        { 
            ParseTree left = Concat(); 
            if(Peek() == '|') 
            { 
                Pop(); 
                ParseTree right = Expression();
                ParseTree exprNode = new ParseTree(ParseTree.NodeType.ALTER, ' ', left, right); //null
                return exprNode;
            } 
            else
                return left; 
        }

        public static bool CheckRegExpPattern(string s)
        {
            char[] regExpChars = "()ab|*?+".ToCharArray();
            foreach (char c in s.Where(c => !regExpChars.Contains(c)))
                return false;
            return true;
        }

        private string Preprocess(string input)
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                output.Append(input[i]);
                if (i == input.Length-1)
                    break;
                if ((char.IsLetterOrDigit(input[i]) || input[i] == ')' || input[i] == '*' || input[i] == '+' || input[i] == '?') && (input[i + 1] != ')' && input[i + 1] != '|' && input[i + 1] != '*' && input[i + 1] != '+' && input[i + 1] != '?'))
                    output.Append('.');
            }
            return output.ToString();
        } 
    }
}