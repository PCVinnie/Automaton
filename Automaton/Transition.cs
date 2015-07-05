using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Transition
    {
        string fromState;
        string toState;
        char symbol;

        public Transition(string fromState, char symbol, string toState)
        {
            this.fromState = fromState;
            this.toState = toState;
            this.symbol = symbol;
        }

        public string getFromState()
        {
            return fromState;
        }

        public string getToState()
        {
            return toState;
        }

        public char getSymbol()
        {
            return symbol;
        }

        public string print()
        {
            return "(" + fromState + "," + symbol + ") -->" + toState;
        }
    }
}
