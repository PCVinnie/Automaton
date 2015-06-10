using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class State
    {
        char stateId;
        int condition;

        public State(char stateId, int condition)
        {
            this.stateId = stateId;
            this.condition = condition;
        }

        public char getStateId()
        {
            return stateId;
        }

        public int getCondition()
        {
            return condition;
        }

    }
}
