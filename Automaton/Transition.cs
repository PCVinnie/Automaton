using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Transition
    {
        State fromState;
        State toState;
        char symbol;

        public Transition(State fromState, char symbol, State toState)
        {
            this.fromState = fromState;
            this.toState = toState;
            this.symbol = symbol;
        }

        public string print()
        {
            return "(" + fromState.getStateId() + "," + symbol + ") -->" + toState.getStateId();
        }
    }
}
