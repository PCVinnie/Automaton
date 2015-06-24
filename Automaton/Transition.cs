using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Transition
    {
        char fromState;
        char toState;
        char symbol;
        public static char EPSILON = '$';

        public Transition(char fromState, char symbol, char toState)
        {
            this.fromState = fromState;
            this.toState = toState;
            this.symbol = symbol;
        }

        public char getFromState()
        {
            return fromState;
        }

        public char getToState()
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
