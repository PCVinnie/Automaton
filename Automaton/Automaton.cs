using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Automaton
    {
        List<Transition> transitions = new List<Transition>();
        string[,] matrix = new string[30, 6];

        char[] symbols;
        string[] beginState;
        string endState;
        int up = 0;

        public Automaton(char[] symbols, string[] beginState, string endState)
        {
            this.symbols = symbols;
            this.beginState = beginState;
            this.endState = endState;

        }

        public void addTransition(Transition t)
        {
            transitions.Add(t);
        }

        public void ndfaToDFA()
        {

            foreach (Transition transition in transitions)
            {
                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                {

                    if (symbols[symbolNr] == transition.getSymbol())
                    {
                        for (int i = 0; i < beginState.Length; i++)
                        {
                            if (beginState[i] == transition.getFromState())
                            {
                                matrix[i, 0] = beginState[i];                          
                            }
                        }
                        for (int row = 0; row < matrix.GetLength(0); row++)
                        {
                            if (matrix[row, 0] == transition.getFromState()) {
                                matrix[row, symbolNr + 1] += transition.getToState();
                            }
                        }
                    }
                }
            }

            addNewState();

            string split = "";
            string split1 = "";
            for (int solution = 0; solution < countStates(); solution++)
            {
                for (int row = beginState.Length; row < matrix.GetLength(0); row++)
                {
                    // Maakt een splitsing bij karakters groter dan 2 en slaat dit op in een string array.
                    if (matrix[row, 0] != null && matrix[row, 0].Length > 1)
                    {
                        split = matrix[row, 0];

                        //Controleert de transition voegt de fromstate toe aan de tweedimensionale array.
                        foreach (Transition transition in transitions)
                        {
                            for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                            {
                                if (symbols[symbolNr] == transition.getSymbol())
                                {
                                    for (int index = 0; index < split.Length; index++)
                                    {
                                        if (split[index].ToString() == transition.getFromState())
                                        {
                                            matrix[row, symbolNr + 1] += transition.getToState();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int column = 0; column < symbols.Length+1; column++)
                    {

                        split1 = matrix[row, column];
                        if (split1 != null)
                        {
                
                            matrix[row, column] = String.Join("", split1.Distinct());

                        }
                    }
                }

                addNewState();

            }


                // Kijkt naar het omgekeerde van A <- B en voegt hiervoor een toestand toe aan de lijst.
                /*
                foreach (Transition transition2 in transitions)
                    if (transition2.getFromState() == transition.getToState() && transition2.getSymbol() == transition.getSymbol())
                        for (int i = 0; i < matrix.GetLength(0); i++)
                            if (matrix[i, 0] == transition.getToState())
                                matrix[i, symbolNr + 1] += transition.getFromState();

            }
        }
    }
                */
                // Vult de lege plaatsen op met een lege string. (Lege verzameling)
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < symbols.Length; j++)
                    {
                        if (matrix[i, j] == null)
                        {
                            matrix[i, j] = " ";
                        }
                    }
                }

            printDFATable();
        }

        public void addNewState() {

            int count = 0;
            for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
            {
                for (int row1 = 0; row1 < matrix.GetLength(0); row1++)
                {
                    for (int row2 = 0; row2 < matrix.GetLength(0); row2++)
                    {

                        if (matrix[row1, symbolNr + 1] == matrix[row2, 0])
                        {
                            count++;
                        }

                    }

                    if (count < 1)
                    {
                        matrix[up + beginState.Length, 0] = matrix[row1, symbolNr + 1];
                        up++;
                    }
                    count = 0;
                }
            }
        
        }

        public int countStates()
        {
            string states = "";
            string unique = "";

            foreach (Transition transition in transitions)
            {
                states += transition.getFromState();
                states += transition.getToState();
                unique = String.Join("", states.Distinct());
            }

            return (int)Math.Pow(Convert.ToDouble(states.Length), Convert.ToDouble(symbols.Length));
        }

        public void printDFATable()
        {

            string symbol = "";
            string table = "";
            string space = "";
            bool asPrint = true;
            int totLength = 0;
            int strLength = 0;

            for (int i = 0; i < matrix.GetLength(0); i++) {
                
                for (int j = 0; j < symbols.Length; j++) {

                    for (int l = 0; l < matrix.GetLength(0); l++)
                        if (matrix[l, 0].Length > strLength)
                            strLength = matrix[l, 0].Length;

                    totLength = strLength - matrix[i, j].Length;

                    for (int m = 0; m < totLength; m++)
                        space += " ";

                    // Table
                    if (matrix[i, j + 1] == null)
                        table += space + " |  " + matrix[i, j + 1];
                    else
                        table += space + " | " + matrix[i, j + 1];
                    space = "";
                
                }

                if (asPrint == true) {

                    totLength = strLength - 1;

                    for (int k = 0; k < totLength; k++)
                        space += " ";

                    // Symbols
                    for (int l = 0; l < symbols.Length; l++)
                        symbol += space + " | " + symbols[l];

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" " + symbol);
                    Console.ResetColor();

                    symbol = "";
                    space = "";
                    asPrint = false;

                }
                Console.WriteLine(matrix[i, 0] + table);
                table = "";
            }
        }

        List<Transition> getTransitions()
        {
            return transitions;
        }

        public void printTransitions()
        {
            foreach (Transition obj in transitions)
            {
                Console.WriteLine(obj.print());
            }
        }
    }
}
