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
            string[] beginState = { "A" };
            string endState = "F";

            Automaton automaton = new Automaton(symbols, beginState);

            /*------------------Terminal------------------*/

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while (true)
            {
                Console.WriteLine("Kies uit 1 t/m 4:");
                Console.WriteLine("1. Gebruik NDFA->DFA voorbeeld");
                Console.WriteLine("2. Gebruik NDFA<->Reguliere Grammatica voorbeeld");
                Console.WriteLine("3. Handmatige invoer NDFA->DFA en opslaan .txt bestand");
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
                            automaton.addTransition(new Transition("D", 'a', "E"));
                            automaton.addTransition(new Transition("D", 'b', "E"));
                            automaton.addTransition(new Transition("E", 'a', "A"));
                            automaton.addTransition(new Transition("D", '$', "E"));

                            /* Print NDFA */
                            Console.WriteLine("NDFA:");
                            automaton.printTransitions();
                            Console.WriteLine(" ");

                            /* Print NDFA -> DFA */
                            Console.WriteLine("NDFA->DFA");
                            DFA dfa = new DFA(automaton.ndfaToDFA(),symbols);
                            dfa.printDFA();
                            Console.WriteLine(" ");

                            automaton.clearTransitions();
                            automaton.clearMatrix();
                            automaton.clearPosition();
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

                            /* Print NDFA */
                            Console.WriteLine("NDFA:");
                            automaton.printTransitions();
                            Console.WriteLine(" ");

                            /* Print NDFA <-> Reguliere Grammatica */
                            Console.WriteLine("NDFA<->Reguliere Grammatica");
                            RegGra regGra = new RegGra(automaton.getTransitions());
                            regGra.printRegGra();
                            Console.WriteLine(" ");

                            regGra.clearTransitions();
                            break;
                        case 3:
                            char symbol = ' ';
                            char symbolTmp  = ' ';
                            int number = 0;
                            bool asBeginState = true;
                            bool asFileIOError = false;
                            bool asSymbolError = true;
                            string begin = "";
                            string end = "";
                            Console.WriteLine("Geef de aantal toestanden op");
                            if (int.TryParse(Console.ReadLine(), out number))
                            {
                                if (number > 0)
                                {
                                    for (int nrOfStates = 0; nrOfStates < number; nrOfStates++)
                                    {
                                        if (asBeginState == true)
                                        {
                                            Console.WriteLine("De begintoestand is: A");
                                            begin = "A";
                                            asBeginState = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Geef een begintoestand: A, B, C enz.");
                                            string beginTmp = Console.ReadLine();
                                            if (string.IsNullOrEmpty(beginTmp))
                                            {
                                                beginTmp = " ";
                                            }
                                            
                                            if (Char.IsLetter(beginTmp[0]) || beginTmp == " ") {
                                                begin = beginTmp;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Verkeerde invoer!");
                                                Console.WriteLine(" ");
                                                break;
                                            }
                                        }

                                        Console.WriteLine("Geef een symbool, kies: a, b of $");
                                        if (char.TryParse(Console.ReadLine(), out symbolTmp))
                                        {
                                            for (int i = 0; i < symbols.Length; i++)
                                            {
                                                if (symbolTmp == symbols[i])
                                                {
                                                    symbol = symbolTmp;
                                                    asSymbolError = false;
                                                }
                                            }

                                            if (asSymbolError == true)
                                            {
                                                asFileIOError = true;
                                                Console.WriteLine("Verkeerde invoer!");
                                                Console.WriteLine(" ");
                                                break;
                                            }
                                            asSymbolError = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Verkeerde invoer!");
                                            Console.WriteLine(" ");
                                            asFileIOError = true;
                                            break;
                                        }
                                        Console.WriteLine("Geef een eindtoestand: A, B, C enz.");
                                        string endTmp = Console.ReadLine();
                                        if (string.IsNullOrEmpty(endTmp))
                                        {
                                            endTmp = " ";
                                        }

                                        if (Char.IsLetter(endTmp[0]) || endTmp == " ") {
                                            end = endTmp;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Verkeerde invoer!");
                                            Console.WriteLine(" ");
                                            asFileIOError = true;
                                        }

                                        automaton.addTransition(new Transition(begin.ToUpper(), symbol, end.ToUpper()));
                                    }

                                    if (asFileIOError == false)
                                    {
                                        /* Schrijft DFA naar .txt bestand */
                                        automaton.writeFile("dfa.txt");
                                        Console.WriteLine("DFA is opgeslagen in dfa.txt");
                                        Console.WriteLine(" ");

                                        /* Print NDFA */
                                        Console.WriteLine("NDFA:");
                                        automaton.printTransitions();
                                        Console.WriteLine(" ");

                                        /* Print NDFA -> DFA */
                                        Console.WriteLine("NDFA->DFA");
                                        DFA dfaFromUserInput = new DFA(automaton.ndfaToDFA(), symbols);
                                        dfaFromUserInput.printDFA();
                                        Console.WriteLine(" ");
                                    }
                                    asFileIOError = false;

                                    automaton.clearTransitions();
                                    automaton.clearMatrix();
                                    automaton.clearPosition();
                                }
                                else
                                {
                                    Console.WriteLine("Geef een getal op groter dan 0!");
                                    Console.WriteLine(" ");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Verkeerde invoer!");
                                Console.WriteLine(" ");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter a regular expression:");
                            string pattern = Console.ReadLine();
                            RegexParser parser;
                            if (RegexParser.CheckRegExpPattern(pattern))
                                parser = new RegexParser(pattern);
                            else
                                Console.WriteLine("Forbidden character(s) detected!");
                            break;
                        default:
                            Console.WriteLine("Kies een getal van 1 t/m 4.");
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