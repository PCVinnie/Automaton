using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class NFA
    {
        public int initial;
        public int final;
        private int size;

        public List<char> inputs;
        public char[][] transitionTable;

        public enum Constants
        {
            EPSILON = 'ε',
            NONE = '\0'
        }

        public NFA(NFA nfa)
        {
            initial = nfa.initial;
            final = nfa.final;
            size = nfa.size;
            inputs = nfa.inputs;
            transitionTable = nfa.transitionTable;
        }

        public NFA(int _size, int _initial, int _final)
        {
            initial = _initial;
            final = _final;
            size = _size;

            inputs = new List<char>();

            // Initializes transitionTable with an "empty graph", no transitions between its
            // states
            transitionTable = new char[size][];

            for (int i = 0; i < size; ++i)
                transitionTable[i] = new char[size];
        }

        public void AddTrans(int from, int to, char input)
        {
            transitionTable[from][to] = input;

            if (input != (char)Constants.EPSILON)
                inputs.Add(input);
        }

        public void FillStates(NFA other)
        {
            for (int i = 0; i < other.size; ++i)
                for (int j = 0; j < other.size; ++j)
                    transitionTable[i][j] = other.transitionTable[i][j];

            foreach (char c in other.inputs)
                inputs.Add(c);
        }

        public void ShiftStates(int shift)
        {
            int newSize = size + shift;

            if (shift < 1)
                return;

            // Creates a new, empty transition table (of the new size).
            char[][] newTransitionTable = new char[newSize][];

            for (int i = 0; i < newSize; ++i)
                newTransitionTable[i] = new char[newSize];

            // Copies all the transitions to the new table, at their new locations.
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    newTransitionTable[i + shift][j + shift] = transitionTable[i][j];

            // Updates the NFA members.
            size = newSize;
            initial += shift;
            final += shift;
            transitionTable = newTransitionTable;
        }

        public void AppendEmptyState()
        {
            transitionTable = Resize(transitionTable, size + 1);
            size += 1;
        }

        private static char[][] Resize(char[][] transitionTable, int newSize)
        {
            char[][] newTransitionTable = new char[newSize][];

            for (int i = 0; i < newSize; ++i)
                newTransitionTable[i] = new char[newSize];

            for (int i = 0; i <= transitionTable.Length - 1; i++)
                for (int j = 0; j <= transitionTable[i].Length - 1; j++)
                {
                    if (transitionTable[i][j] != '\0')
                        newTransitionTable[i][j] = transitionTable[i][j];
                }
            return newTransitionTable;
        }

        public HashSet<int> Move(HashSet<int> states, char inp)
        {
            HashSet<int> result = new HashSet<int>();

            // For each state in the set of states
            foreach (int state in states)
            {
                int i = 0;

                // For each transition from this state
                foreach (char input in transitionTable[state])
                {
                    // If the transition is on input inp, add it to the resulting set
                    if (input == inp)
                    {
                        int u = Array.IndexOf(transitionTable[state], input, i);
                        result.Add(u);
                    }

                    i = i + 1;
                }
            }

            return result;
        }

        public void Show()
        {
            Console.WriteLine("This NFA has {0} states: 0 - {1}", size, size - 1);
            Console.WriteLine("The initial state is {0}", initial);
            Console.WriteLine("The final state is {0}\n", final);

            for (int from = 0; from < size; ++from)
            {
                for (int to = 0; to < size; ++to)
                {
                    char input = transitionTable[from][to];

                    if (input != (char)Constants.NONE)
                    {
                        Console.Write("{0} -> {1}: ", from, to);

                        if (input == (char)Constants.EPSILON)
                            Console.Write("EPSILON\n");
                        else
                            Console.Write("{0}\n", input);
                    }
                }
            }
            Console.Write("\n\n");
        }

        public static NFA TreeToNFA(ParseTree tree)
        {
            switch (tree.type)
            {
                case ParseTree.NodeType.CHAR:
                    return BuildNFABasic(tree.data);
                case ParseTree.NodeType.ALTER:
                    return BuildNFAAlter(TreeToNFA(tree.left), TreeToNFA(tree.right));
                case ParseTree.NodeType.CONCAT:
                    return BuildNFAConcat(TreeToNFA(tree.left), TreeToNFA(tree.right));
                case ParseTree.NodeType.STAR:
                    return BuildNFAStar(TreeToNFA(tree.left));
                case ParseTree.NodeType.PLUS:
                    return BuildNFAPlus(TreeToNFA(tree.left));
                case ParseTree.NodeType.QUESTION:
                    return BuildNFAAlter(TreeToNFA(tree.left), BuildNFABasic((char)Constants.EPSILON));
                default:
                    return null;
            }
        }

        public static NFA BuildNFABasic(char input)
        {
            NFA basic = new NFA(2, 0, 1);
            basic.AddTrans(0, 1, input);
            return basic;
        }

        public static NFA BuildNFAAlter(NFA nfa1, NFA nfa2)
        {
            // How this is done: the new nfa must contain all the states in
            // nfa1 and nfa2, plus a new initial and final states. 
            // First will come the new initial state, then nfa1's states, then
            // nfa2's states, then the new final state

            // make room for the new initial state
            nfa1.ShiftStates(1);

            // make room for nfa1
            nfa2.ShiftStates(nfa1.size);

            // create a new nfa and initialize it with (the shifted) nfa2
            NFA newNFA = new NFA(nfa2);

            // nfa1's states take their places in new_nfa
            newNFA.FillStates(nfa1);

            // Set new initial state and the transitions from it
            newNFA.AddTrans(0, nfa1.initial, (char)Constants.EPSILON);
            newNFA.AddTrans(0, nfa2.initial, (char)Constants.EPSILON);

            newNFA.initial = 0;

            // Make up space for the new final state
            newNFA.AppendEmptyState();

            // Set new final state
            newNFA.final = newNFA.size - 1;

            newNFA.AddTrans(nfa1.final, newNFA.final, (char)Constants.EPSILON);
            newNFA.AddTrans(nfa2.final, newNFA.final, (char)Constants.EPSILON);

            return newNFA;
        }

        public static NFA BuildNFAConcat(NFA nfa1, NFA nfa2)
        {
            // How this is done: First will come nfa1, then nfa2 (its initial state replaced
            // with nfa1's final state)
            nfa2.ShiftStates(nfa1.size - 1);

            // Creates a new NFA and initialize it with (the shifted) nfa2
            NFA newNFA = new NFA(nfa2);

            // nfa1's states take their places in newNFA
            // note: nfa1's final state overwrites nfa2's initial state,
            // thus we get the desired merge automatically (the transition
            // from nfa2's initial state now transits from nfa1's final state)
            newNFA.FillStates(nfa1);

            // Sets the new initial state (the final state stays nfa2's final state,
            // and was already copied)
            newNFA.initial = nfa1.initial;

            return newNFA;
        }

        public static NFA BuildNFAStar(NFA nfa)
        {
            // Makes room for the new initial state
            nfa.ShiftStates(1);

            // Makes room for the new final state
            nfa.AppendEmptyState();

            // Adds new transitions
            nfa.AddTrans(nfa.final, nfa.initial, (char)Constants.EPSILON);
            nfa.AddTrans(0, nfa.initial, (char)Constants.EPSILON);
            nfa.AddTrans(nfa.final, nfa.size - 1, (char)Constants.EPSILON);
            nfa.AddTrans(0, nfa.size - 1, (char)Constants.EPSILON);

            nfa.initial = 0;
            nfa.final = nfa.size - 1;

            return nfa;
        }

        public static NFA BuildNFAPlus(NFA nfa)
        {
            // Makes room for the new initial state
            nfa.ShiftStates(1);

            // Makes room for the new final state
            nfa.AppendEmptyState();

            // Adds new transitions
            nfa.AddTrans(nfa.final, nfa.initial, (char)Constants.EPSILON);
            nfa.AddTrans(0, nfa.initial, (char)Constants.EPSILON);
            nfa.AddTrans(nfa.final, nfa.size - 1, (char)Constants.EPSILON);

            nfa.initial = 0;
            nfa.final = nfa.size - 1;

            return nfa;
        }
    }
}