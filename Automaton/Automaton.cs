using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automaton
{
    class Automaton
    {
        List<Transition> transitions;
     
        char[] symbols;
        string[,] matrix;
        string[] beginState;
        int up = 0;

        public Automaton(char[] symbols, string[] beginState)
        {
            this.symbols = symbols;
            this.beginState = beginState;

            matrix = new string[15, symbols.Length+1];
            transitions = new List<Transition>();
        }

        public void addTransition(Transition t)
        {
            transitions.Add(t);
        }

        public string[,] ndfaToDFA()
        {

            string splitStates1 = "";
            string splitStates2 = "";
            char[] tmp = { };
            foreach (Transition transition in transitions)
            {
                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                {
                    if (symbols[symbolNr] == transition.getSymbol())
                    {
                        // Voegt de begintoestanden aan de dimensie 0.
                        for (int i = 0; i < beginState.Length; i++)
                        {
                            if (beginState[i] == transition.getFromState())
                            {
                                matrix[i, 0] = beginState[i];                          
                            }
                        }
                        addStateToDimension();
                    }
                }
            }

            // Voegt nieuwe states aan dimensie 0.
            addNewState();

            for (int solution = 0; solution < countStates(); solution++)
            {
                for (int row = beginState.Length; row < matrix.GetLength(0); row++)
                {            
                    if (matrix[row, 0] != null)
                    {
                        // Maakt een splitsing bij karakters groter dan 1 en slaat dit op in een string array.
                        if (matrix[row, 0].Length > 1)
                        {
                            splitStates1 = matrix[row, 0];

                            //Controleert de transition voegt de fromstate toe aan de tweedimensionale array.
                            foreach (Transition transition in transitions)
                            {
                                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                                {
                                    if (symbols[symbolNr] == transition.getSymbol())
                                    {
                                        for (int index = 0; index < splitStates1.Length; index++)
                                        {
                                            if (splitStates1[index].ToString() == transition.getFromState())
                                            {
                                                matrix[row, symbolNr + 1] += transition.getToState();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        epsilonClosure();
                                    }
                                }
                            }

                        }
                        else
                        {
                            addStateToDimension();
                        }
                    }
                }

                // Filtert de dubbele toestanden eruit en sorteert de letters.
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int column = 0; column < symbols.Length+1; column++)
                    {
                        splitStates2 = matrix[row, column];
                        if (splitStates2 != null)
                        {
                            tmp = splitStates2.ToCharArray();
                            Array.Sort(tmp);
                            matrix[row, column] = String.Join("", tmp.Distinct());
                        }
                    }
                }
                addNewState();
            }
            addEmptyString();

            return matrix;
        }

        public void addStateToDimension()
        {
            // Controleert de begintoestanden en voegt de eindtoestanden toe aan de dimensies a, b enz.
            foreach (Transition transition in transitions)
            {
                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                {
                    if (symbols[symbolNr] == transition.getSymbol())
                    {
                        for (int row = 0; row < matrix.GetLength(0); row++)
                        {
                            if (matrix[row, 0] == transition.getFromState()) 
                            {
                                matrix[row, symbolNr + 1] += transition.getToState();
                            }
                        }
                    }
                }
            }
        }

        public void epsilonClosure()
        {
            string splitStates1 = "";

            foreach (Transition transition in transitions) 
            {
                if (transition.getSymbol() == '$')
                {
                    foreach (Transition transition2 in transitions)
                    {
                        if (transition2.getSymbol() != '$')
                        {
                            if (transition.getToState() == transition2.getFromState())
                            {
                                for (int symbolNr = 0; symbolNr < symbols.Length; symbolNr++)
                                {
                                    if (symbols[symbolNr] == transition2.getSymbol())
                                    {
                                        for (int row = 0; row < matrix.GetLength(0); row++)
                                        {
                                            if (matrix[row, 0] != null)
                                            {
                                                // Maakt een splitsing bij karakters groter dan 1 en slaat dit op in een string array.
                                                if (matrix[row, 0].Length > 1)
                                                {
                                                    splitStates1 = matrix[row, 0];
                                                    for (int index = 0; index < splitStates1.Length; index++)
                                                    {
                                                        if (splitStates1[index].ToString() == transition.getFromState())
                                                        {
                                                            matrix[row, symbolNr + 1] += transition2.getToState();
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // Indien het gaat over één enkele toestand.
                                                    if (matrix[row, 0] == transition.getFromState())
                                                    {
                                                        matrix[row, symbolNr + 1] += transition2.getToState();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }            
                }
            }
        }

        public void addEmptyString()
        {
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
        }

        public void addNewState() 
        {
            int count = 0;
            // Controleert op dubbele toestanden tussen dimensie 0 en de dimensies a, b enz. 
            // Als er geen dubbele toestanden zijn voegt het een toestand toe.
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

            // Berekent de maximaal aantal toestanden die mogelijk zijn.
            foreach (Transition transition in transitions)
            {
                states += transition.getFromState();
                states += transition.getToState();
                unique = String.Join("", states.Distinct());
            }

            return (int)Math.Pow(Convert.ToDouble(unique.Length), Convert.ToDouble(symbols.Length));
        }

        public List<Transition> getTransitions()
        {
            return transitions;
        }

        public void clearTransitions()
        {
            transitions.Clear();
        }

        public void clearMatrix()
        {
            Array.Clear(matrix, 0, matrix.Length);
        }

        public void clearPosition()
        {
            up = 0;
        }

        public void printTransitions()
        {
            foreach (Transition obj in transitions)
            {
                Console.WriteLine(obj.print());
            }
        }

        public void writeFile(string path) 
        {
            //if (File.Exists(path))
            //{
                using (StreamWriter sw = File.CreateText(path))
                {
                    string txt = "";
                    for (int symbol = 0; symbol < symbols.Length; symbol++) {
                        txt += " | " + symbols[symbol];
                    }
                    sw.WriteLine(txt);

                    string columns = "";

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < symbols.Length; j++)
                        {
                            columns += " | " + matrix[i, j+1];
                        }
                        sw.WriteLine(">" + matrix[i, 0] + " | " + columns);
                        columns = "";
                    }
                }
            //}
        }

        public void readFile(string path) 
        {
            using (StreamReader sr = File.OpenText(path)) {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {

                        Console.WriteLine(s);

                }
            }
        }
    }
}