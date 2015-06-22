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
        char[,] twoArray = new char[9, 3];

        char[] symbols;
        char beginState;
        char endState;

        public Automaton(char[] symbols, char beginState, char endState)
        {
            this.symbols = symbols;
            this.beginState = beginState;
            this.endState = endState;

        }

        public void addTransition(Transition t)
        {
            transitions.Add(t);
        }

        public void check()
        {
            twoArray[0,0] = beginState;
            int count = 0;
            int newRow = 0;
            char[] list = new char[10];

            foreach (Transition transition in transitions)
            {

                foreach (char symbol in symbols)
                {

                    if (symbol == transition.getSymbol())
                    {

                        if (symbol == 'a')
                        {

                            // Voegt een toestand aan de lijst toe bijvoorbeeld: A -> B
                            for (int i = 0; i < twoArray.GetLength(0); i++)
                                if (twoArray[i, 0] == transition.getFromState())
                                    twoArray[i, 1] = transition.getToState();          

                            //---------------------------------------------------------------------//
                            for (int i = 0; i < twoArray.GetLength(0); i++) 
                                if (twoArray[i, 0] == transition.getToState())
                                    count++;

                            if (count < 1)
                                 newRow++; twoArray[newRow, 0] = transition.getToState();
                            count = 0;


                            foreach (Transition transition2 in transitions)   
                                if (transition2.getFromState() == transition.getToState() && transition2.getSymbol() == transition.getSymbol())
                                    for (int i = 0; i < twoArray.GetLength(0); i++)
                                        if (twoArray[i, 0] == transition.getToState())
                                            twoArray[i, 1] = transition.getFromState();
                  
                        }
                        else
                        {

                            // Voegt een toestand aan de lijst toe bijvoorbeeld: A -> B
                            for (int i = 0; i < twoArray.GetLength(0); i++)
                                if (twoArray[i, 0] == transition.getFromState())
                                    twoArray[i, 2] = transition.getToState();                

                            // Controleert of er geen dubbele toestanden zijn op column 0, zo niet dan voegt die een toestand toe.
                            for (int i = 0; i < twoArray.GetLength(0); i++)
                                if (twoArray[i, 0] == transition.getToState())
                                    count++;

                            if (count < 1)
                                newRow++;                                      
                                twoArray[newRow, 0] = transition.getToState();
                            count = 0;

                            // Kijkt naar het omgekeerde van A <- B en voegt hiervoor een toestand toe aan de lijst.
                            foreach (Transition transition2 in transitions)
                                if (transition2.getFromState() == transition.getToState() && transition2.getSymbol() == transition.getSymbol())
                                    for (int i = 0; i < twoArray.GetLength(0); i++)
                                        if (twoArray[i, 0] == transition.getToState())
                                            twoArray[i, 2] = transition.getFromState();

                        }
                    }
                }
            }
            
            //Print de twee dimensionale array in terminal.
            int rowLength = twoArray.GetLength(0);
            int colLength = twoArray.GetLength(1);

            Console.WriteLine("Aantal NDFA overgangen: " + transitions.Count);
            Console.WriteLine("Aantal toestanden: " + rowLength);
            Console.WriteLine("Aantal symbolen: " + colLength);

            for (int i = 0; i < rowLength; i++) {
                Console.WriteLine(i + ". " + twoArray[i, 0] + " | " + twoArray[i, 1] + " | " + twoArray[i, 2]);
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
