using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class RegGra : Representation
    {
        List<Transition> transitions;

        public RegGra(List<Transition> transitions)
        {
            this.transitions = transitions;
        }

        public void printRegGra()
        {
            string unique = "";
            string tmp = "";
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
                Console.WriteLine(unique[state].ToString() + "->" + txt[state]);
            }
        }
    }
}
