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
        List<List<char>> matrix = new List<List<char>>();
        string[,] twoArray = new string[9, 3];

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
            twoArray[0, 0] = beginState;
            int count = 0;
            int newState = 0;
            int newRow = 0;
            char[] list = new char[10];

            foreach (Transition transition in transitions)
            {

                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                {

                    if (symbols[symbolNr] == transition.getSymbol())
                    {

                            // Voegt een toestand aan de lijst toe bijvoorbeeld: A -> B
                            for (int row = 0; row < twoArray.GetLength(0); row++)
                                if (twoArray[row, 0] == transition.getFromState())
                                {
                                    twoArray[row, symbolNr+1] += transition.getToState();
                                    newState = row;
                                }

                            // Controleert of er geen dubbele toestanden zijn op column 0, zo niet dan voegt die een toestand toe.
                            for (int i = 0; i < twoArray.GetLength(0); i++)
                                if (twoArray[i, 0] == twoArray[newState, symbolNr+1])
                                    count++;

                            if (count < 1)
                            {
                                newRow++;
                                twoArray[newRow, 0] = twoArray[newState, symbolNr+1];
                            }
                            count = 0;

                            // Kijkt naar het omgekeerde van A <- B en voegt hiervoor een toestand toe aan de lijst.
                            foreach (Transition transition2 in transitions)
                                if (transition2.getFromState() == transition.getToState() && transition2.getSymbol() == transition.getSymbol())
                                    for (int i = 0; i < twoArray.GetLength(0); i++)
                                        if (twoArray[i, 0] == transition.getToState())
                                            twoArray[i, symbolNr+1] += transition.getFromState();
                        }
                    }
                }

            // Print de symbolen.
            string symbolStr = "";
            string line = "";
            for (int i = 0; i < symbols.Length; i++) {
                line += "    " + symbols[i];
                symbolStr += "------";
            }
            Console.WriteLine(line);
            Console.WriteLine(symbolStr);

            // Print de twee dimensionale array in terminal.
            int rowLength = twoArray.GetLength(0);
            string table = "";
            for (int i = 0; i < rowLength; i++) {
                for (int j = 0; j < symbols.Length; j++)
                {
                    table += " | " + twoArray[i, j+1];
                }
                Console.WriteLine(twoArray[i, 0] + table);
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
