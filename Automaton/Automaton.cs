using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Automaton
    {

        List<State> states = new List<State>();
        List<char> symbols = new List<char>();
        List<Transition> transitions = new List<Transition>();

        public Automaton(List<State> states, List<char> symbols)
        {
            this.states = states;
            this.symbols = symbols;
        }

        public void addTransition(Transition t)
        {
            transitions.Add(t);
        }

        public void printTransitions()
        {
            foreach (Transition obj in transitions) {
                Console.WriteLine(obj.print());
            }
        }

    }
}
