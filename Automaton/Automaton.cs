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
        string[,] matrix = new string[9, 6];

        char[] symbols;
        string beginState;
        string endState;

        public Automaton(char[] symbols, string beginState, string endState)
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
            matrix[0, 0] = beginState;
            int count = 0;
            int newState = 0;
            int newRow = 0;
            int oldState = 0;
            int oldSymbolNr = 0;

            foreach (Transition transition in transitions) {
                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++) {

                    if (symbols[symbolNr] == transition.getSymbol()) {

                        // Voegt een toestand aan de lijst toe bijvoorbeeld: A -> B
                        for (int row = 0; row < matrix.GetLength(0); row++)
                        {
                            if (matrix[row, 0] == transition.getFromState())
                            {
                                matrix[row, symbolNr + 1] += transition.getToState();
                                newState = row;
                            }
                        }

                        // Controleert of er geen dubbele toestanden zijn op column 0, zo niet dan voegt die een toestand toe.
                        for (int i = 0; i < matrix.GetLength(0); i++)
                        {
                            if (matrix[i, 0] == matrix[newState, symbolNr + 1])
                            {
                                count++;
                            }
                        }

                            if (count < 1) {
                                newRow++;
                                matrix[newRow, 0] = matrix[newState, symbolNr + 1];
                            }
                            count = 0;

                        // Kijkt naar het omgekeerde van A <- B en voegt hiervoor een toestand toe aan de lijst.
                        foreach (Transition transition2 in transitions)
                            if (transition2.getFromState() == transition.getToState() && transition2.getSymbol() == transition.getSymbol())
                                for (int i = 0; i < matrix.GetLength(0); i++)
                                    if (matrix[i, 0] == transition.getToState())
                                        matrix[i, symbolNr + 1] += transition.getFromState();
                    }
                }
            }

            // Vult de lege plaatsen op met een lege string. (Lege verzameling)
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < symbols.Length; j++)
                    if (matrix[i, j] == null)
                        matrix[i, j] = " ";

            epsilonClosure();

            printDFATable();
        }

        public void epsilonClosure()
        {
            
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
