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
            char[] symbols = { 'a', 'b' };
            string[] beginState = { "A", "D" };

            Automaton automaton = new Automaton(symbols, beginState);

            /*------------------Terminal------------------*/

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while (true)
            {
                Console.WriteLine("Kies uit 1 t/m 5:");
                Console.WriteLine("1. Gebruik NDFA->DFA voorbeeld");
                Console.WriteLine("2. Gebruik NDFA<->Reguliere Grammatica voorbeeld");
                Console.WriteLine("3. NDFA, DFA en Reguliere Grammatica opslaan in .txt bestand");
                Console.WriteLine("4. Handmatige invoer Reguliere Expressie");

                string userInput = Console.ReadLine();
                int value;
                if (int.TryParse(userInput, out value))
                {
                    switch (value)
                    {
                        case 1:
                            /* NDFA voorbeeld */
                            automaton.addTransition(new Transition("A", 'b', "B"));
                            automaton.addTransition(new Transition("A", 'a', "C"));
                            automaton.addTransition(new Transition("A", 'b', "C"));
                            automaton.addTransition(new Transition("A", 'a', "E"));
                            automaton.addTransition(new Transition("A", 'b', "E"));
                            automaton.addTransition(new Transition("B", 'a', "A"));
                            automaton.addTransition(new Transition("B", 'b', "A"));
                            automaton.addTransition(new Transition("B", 'a', "D"));
                            automaton.addTransition(new Transition("B", 'b', "D"));
                            automaton.addTransition(new Transition("C", 'b', "A"));
                            automaton.addTransition(new Transition("C", 'b', "D"));
                            automaton.addTransition(new Transition("D", 'b', "B"));
                            automaton.addTransition(new Transition("D", 'a', "C"));
                            automaton.addTransition(new Transition("D", 'b', "C"));
                            //automaton.addTransition(new Transition("D", 'a', "E"));
                            //automaton.addTransition(new Transition("D", 'b', "E"));
                            automaton.addTransition(new Transition("E", 'a', "A"));
                            //automaton.addTransition(new Transition("D", '$', "E"));

                            /* Print NDFA. */
                            Console.WriteLine("NDFA:");
                            automaton.printTransitions();
                            Console.WriteLine(" ");

                            /* Print NDFA -> DFA */
                            Console.WriteLine("NDFA->DFA");
                            DFA dfa = new DFA(automaton.ndfaToDFA(),symbols);
                            dfa.printDFA();
                            Console.WriteLine(" ");

                            break;
                        case 2:
                            /* NDFA voorbeeld */
                            automaton.addTransition(new Transition("A", 'b', "B"));
                            automaton.addTransition(new Transition("A", 'a', "C"));
                            automaton.addTransition(new Transition("A", 'b', "C"));
                            automaton.addTransition(new Transition("A", 'a', "E"));
                            automaton.addTransition(new Transition("A", 'b', "E"));
                            automaton.addTransition(new Transition("B", 'a', "A"));
                            automaton.addTransition(new Transition("B", 'b', "A"));
                            automaton.addTransition(new Transition("B", 'a', "D"));
                            automaton.addTransition(new Transition("B", 'b', "D"));
                            automaton.addTransition(new Transition("C", 'b', "A"));
                            automaton.addTransition(new Transition("C", 'b', "D"));
                            automaton.addTransition(new Transition("D", 'b', "B"));
                            automaton.addTransition(new Transition("D", 'a', "C"));
                            automaton.addTransition(new Transition("D", 'b', "C"));
                            automaton.addTransition(new Transition("D", 'a', "E"));
                            automaton.addTransition(new Transition("D", 'b', "E"));

                            /* Print NDFA. */
                            Console.WriteLine("NDFA:");
                            automaton.printTransitions();
                            Console.WriteLine(" ");

                            /* Print NDFA <-> Reguliere Grammatica */
                            Console.WriteLine("NDFA<->Reguliere Grammatica");
                            RegGra regGra = new RegGra(automaton.getTransitions());
                            regGra.printRegGra();
                            Console.WriteLine(" ");
                            break;
                        case 3:
                            /* Schrijft NDFA, DFA en Reguliere Grammatica naar .txt bestand */
                            automaton.writeFile("DFA.txt");
                            Console.WriteLine("DFA is opgeslagen in dfa.txt");
                            Console.WriteLine(" ");
                            break;
                        case 4:
                            Console.WriteLine("Voer een Reguliere Expressie in. Symbolen: ( ) a b | *");
                            string input = Console.ReadLine();
                            RegExp r;
                            if (RegExp.CheckRegExpInput(input))
                            {
                                r = new RegExp(input);
                            }
                            Console.WriteLine("Input error!");
                            break;
                        default:
                            Console.WriteLine("Kies een getal van 1 t/m 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Verkeerde invoer!");
                }
            }
        }
    }
}
