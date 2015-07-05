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
            string oldState = "";
            bool asState = true;
            foreach (Transition transition in transitions) {
                tmp += transition.getFromState();
            }
            unique = String.Join("", tmp.Distinct());
            string[] txt = new string[unique.Length];
            foreach (Transition transition in transitions) 
            {
                if (transition.getFromState() != oldState)
                {
                    asState = false;
                    oldState = transition.getFromState();
                }

                for (int state = 0; state < unique.Length; state++)
                {
                    if (transition.getFromState() == unique[state].ToString())
                    {
                        if (asState)
                        {
                            txt[state] += transition.getSymbol() + transition.getToState();
                        }
                        else
                        {
                            txt[state] += transition.getSymbol() + transition.getToState() + "|";
                        }
                        asState = true;
                    }
                }
            }

            for (int state = 0; state < unique.Length; state++)
            {
                Console.WriteLine(unique[state].ToString() + "->" + txt[state]);
            }
        }

        public void clearTransitions()
        {
            transitions.Clear();
        }
    }
}
