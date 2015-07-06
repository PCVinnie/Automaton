using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class NDFA : Representation
    {
        List<Transition> transitions;
        char[] symbols;

        public NDFA(List<Transition> transitions, char[] symbols)
        {
            this.transitions = transitions;
            this.symbols = symbols;
        }
        /*
        public void printNDFA() 
        {
            string unique = "";
            string tmp = "";

            foreach (Transition transition in transitions) {
                tmp += transition.getFromState();
            }
            unique = String.Join("", tmp.Distinct());
            string[,] matrix = new string[15,4];

            foreach (Transition transition in transitions) 
            {
                for (int state = 0; state < unique.Length; state++)
                {
                    if (transition.getFromState() == unique[state].ToString())
                    {
                        matrix[state, 0] = transition.getFromState() + " | ";
                    }
                }
            }

            for (int state = 0; state < matrix.GetLength(0); state++)
            {
                Console.WriteLine(matrix[state, 0]);
            }
        }
        */
        public void clearTransitions()
        {
            transitions.Clear();
        }
    }
}
