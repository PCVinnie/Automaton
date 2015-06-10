using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class Program
    {
        static void Main(string[] args)
        {

            State stateA = new State('A', 0);
            State stateB = new State('B', 1);
            State stateC = new State('C', 2);

            List<State> myStates = new List<State>();
            myStates.Add(stateA);
            myStates.Add(stateB);
            myStates.Add(stateC);

            List<char> mySymbols = new List<char>();
            mySymbols.Add('a');
            mySymbols.Add('b');

            Automaton automaton = new Automaton(myStates,mySymbols);

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while(true) {



                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                }

            }

        }
    }
}
