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

            State stateA = new State('A', 0); //Start
            State stateB = new State('B', 1); 
            State stateC = new State('C', 2); //End

            List<State> myStates = new List<State>();
            myStates.Add(stateA);
            myStates.Add(stateB);
            myStates.Add(stateC);

            List<char> mySymbols = new List<char>();
            mySymbols.Add('a');
            mySymbols.Add('b');

            Automaton automaton = new Automaton(myStates,mySymbols);

            /*------------------Terminal------------------*/

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while(true) {
                Console.WriteLine("Kies uit 1 t/m 4.");
                Console.WriteLine("1. Gebruik voorbeeld.");
                Console.WriteLine("2. Handmatige invoer.");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        /* NDFA voorbeeld */
                        automaton.addTransition(new Transition(new State('A',0), 'a', new State('B',1)));
                        automaton.addTransition(new Transition(new State('B',1), 'a', new State('E',4)));
                        automaton.addTransition(new Transition(new State('A',0), 'a', new State('C',2)));
                        automaton.addTransition(new Transition(new State('C',2), 'a', new State('D',3)));
                        automaton.addTransition(new Transition(new State('D',3), 'a', new State('E',4)));
                        break;
                    case 2:
                        /* NDFA handmatige invoer */
                        Console.WriteLine("Hoeveel toestanden moeten er aangemaakt worden?");
                        Console.WriteLine("Er moet minimaal 2 toestanden aangemaakt worden!");
                        int numberOfStates = Int32.Parse(Console.ReadLine());     
                        if(numberOfStates > 2) {
                            for(int i = 0; i < numberOfStates; i++) {
                                Console.WriteLine("Van toestand: ");
                                string beginState = Console.ReadLine();
                                Console.WriteLine("Symbool: ");
                                string inputSymbol = Console.ReadLine();
                                Console.WriteLine("Eind toestand: ");
                                string endState = Console.ReadLine();
                                //automaton.addTransition(new Transition(new State(beginState), inputSymbol, new State(endState));
                            }
                        }
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
