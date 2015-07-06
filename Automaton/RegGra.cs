using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automaton
{
    class RegGra : Representation
    {
        private List<Transition> transitions;

        public RegGra(List<Transition> transitions)
        {
            this.transitions = transitions;
        }

        public void printRegGra()
        {
            string unique = "";
            string tmp = "";
            string[] outputFileIO = new string[10];

            foreach (Transition transition in transitions) {
                tmp += transition.getFromState();
            }
            unique = String.Join("", tmp.Distinct());
            string[] txt = new string[unique.Length];
            
            foreach (Transition transition in transitions) 
            {
                for (int state = 0; state < unique.Length; state++)
                {
                    if (transition.getFromState() == unique[state].ToString())
                    {
                        txt[state] += transition.getSymbol() + transition.getToState() + "|";
                    }
                }
            }

            for (int state = 0; state < unique.Length; state++)
            {
                string str = txt[state];
                string output = unique[state].ToString() + "->" + str.Remove(str.Length - 1);
                outputFileIO[state] = output;
                Console.WriteLine(output);
            }

            Console.WriteLine(" ");
            writeFileRegGra("regGra.txt", outputFileIO);
            Console.WriteLine(" ");
        }

        public void writeFileRegGra(string path, string[] outputFileIO)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int state = 0; state < outputFileIO.Length; state++)
                {
                    sw.WriteLine(outputFileIO[state]);
                }
                
                Console.WriteLine("RegGra is opgeslagen in " + path);
            }
        }

        public void clearTransitions()
        {
            transitions.Clear();
        }
    }
}
