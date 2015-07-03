﻿using System;
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
            string[] beginState = { "A" };
            string endState = "F";

            Automaton automaton = new Automaton(symbols, beginState, endState);

            /*------------------Terminal------------------*/

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while(true) {
                Console.WriteLine("Kies uit 1 t/m 4:");
                Console.WriteLine("1. Gebruik NDFA->DFA voorbeeld");
                Console.WriteLine("2. Handmatige invoer NDFA->DFA");
                Console.WriteLine("3. ");
                Console.WriteLine("4. Sluiten");
                Console.WriteLine("4. Handmatige invoer Reguliere Expressie");
                
                string userInput = Console.ReadLine();
                int value;
                if (int.TryParse(userInput, out value)) {
  
                    switch (value) {
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
                            automaton.addTransition(new Transition("D", 'a', "E"));
                            automaton.addTransition(new Transition("D", 'b', "E"));
                            automaton.addTransition(new Transition("E", 'a', "D"));

                            /* Print NDFA. */
                            Console.WriteLine("NDFA:");
                            automaton.printTransitions();
                            Console.WriteLine(" ");

                            /* Print NDFA -> DFA */                         
                            Console.WriteLine("NDFA->DFA");
                            automaton.ndfaToDFA();
                            automaton.printDFATable();
                            Console.WriteLine(" ");
                            break;
                        case 2:
                            /* NDFA handmatige invoer */
                            Console.WriteLine("Hoeveel toestanden moeten er aangemaakt worden?");
                            Console.WriteLine("Er moet minimaal 2 toestanden aangemaakt worden!");
                            int numberOfStates = Int32.Parse(Console.ReadLine());     
                            if(numberOfStates > 2) {
                                for(int i = 0; i < numberOfStates; i++) {
                                    Console.WriteLine("Van toestand: ");
                                    string beginStateInput = Console.ReadLine();
                                    Console.WriteLine("Symbool: ");
                                    string inputSymbol = Console.ReadLine();
                                    Console.WriteLine("Eind toestand: ");
                                    string endStateInput = Console.ReadLine();
                                    //automaton.addTransition(new Transition(new State(beginState), inputSymbol, new State(endState));
                                }
                            }
                            break;
                        case 3:
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
                            Console.WriteLine("Kies een getal van 1 t/m 4.");
                            break;     
                    }
                } else {
                    Console.WriteLine("Verkeerde invoer!");
                }
            }
        }
    }
}
