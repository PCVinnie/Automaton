using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class RegExp : Representation
    {
        Stack<NFATable> OperandStack = new Stack<NFATable>();
        HashSet<char> InputSet = new HashSet<char>();
        string input;
        int nextStateId;

        public RegExp(string s)
        {
            this.input = s;
        }

        private class RXState
        {
            int Id;
            List<Tuple<RXState, char>> Transitions = new List<Tuple<RXState, char>>();

            public RXState(int id) 
            { 
                this.Id = id; 
            }

            public void AddTransition(RXState rxt, char symbol) 
            {
                Transitions.Add(new Tuple<RXState, char>(rxt, symbol));
            }
        }

        private class NFATable
        {
            public Stack<RXState> RXStates = new Stack<RXState>();
        }

        public void ToNDFA(string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '(':
                        break;
                    case ')':
                        break;
                    case 'a':
                        Push('a');
                        break;
                    case 'b':
                        Push('b');
                        break;
                    case '|':
                        break;
                    case '*':
                        break;
                }
            }
        }

        public void Push(char symbol)
        {
            RXState s0 = new RXState(nextStateId++);
            RXState s1 = new RXState(nextStateId++);
            s0.AddTransition(s1, 'a');

            NFATable NfaTable = new NFATable();
            NfaTable.RXStates.Push(s0);
            NfaTable.RXStates.Push(s1);

            OperandStack.Push(NfaTable);
            InputSet.Add(symbol);
        }

        public static bool CheckRegExpInput(string s)
        {
            char[] regExpChars = "()ab|*".ToCharArray();
            foreach (char c in s.Where(c => !regExpChars.Contains(c)))
                return false;
            return true;
        }
    }
}