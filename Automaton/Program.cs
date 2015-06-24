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
            State stateA = new State('A', (int)State.Type.START);
            State stateB = new State('B', (int)State.Type.NORMAL);
            State stateC = new State('C', (int)State.Type.END);

            List<State> myStates = new List<State>();
            myStates.Add(stateA);
            myStates.Add(stateB);
            myStates.Add(stateC);

            List<char> mySymbols = new List<char>();
            mySymbols.Add('a');
            mySymbols.Add('b');

            Automaton automaton = new Automaton(myStates, mySymbols);

            /*------------------Terminal------------------*/

            Console.WriteLine("Formele methode");
            Console.WriteLine("Vincent Stout & Lesley van Hoek");
            Console.WriteLine("-------------------------------");

            while (true)
            {
                Console.WriteLine("Kies uit 1 t/m 4.");
                Console.WriteLine("1. Gebruik NDFA voorbeeld.");
                Console.WriteLine("2. Handmatige invoer NDFA.");
                Console.WriteLine("3. Print NDFA.");
                Console.WriteLine("4. Test RegExp");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        /* NDFA voorbeeld */
                        automaton.addTransition(new Transition(new State('A', 0), 'a', new State('B', 1)));
                        automaton.addTransition(new Transition(new State('B', 1), 'b', new State('E', 4)));
                        automaton.addTransition(new Transition(new State('A', 0), 'b', new State('C', 2)));
                        automaton.addTransition(new Transition(new State('C', 2), 'a', new State('D', 3)));
                        automaton.addTransition(new Transition(new State('D', 3), 'b', new State('E', 4)));
                        Console.WriteLine("NDFA voorbeeld toegevoegd.");
                        Console.WriteLine(" ");
                        break;
                    case 2:
                        /* NDFA handmatige invoer */
                        Console.WriteLine("Hoeveel toestanden moeten er aangemaakt worden?");
                        Console.WriteLine("Er moet minimaal 2 toestanden aangemaakt worden!");
                        int numberOfStates = Int32.Parse(Console.ReadLine());
                        if (numberOfStates > 2)
                        {
                            for (int i = 0; i < numberOfStates; i++)
                            {
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
                        /* Print NDFA. */
                        Console.WriteLine("NDFA:");
                        automaton.printTransitions();
                        Console.WriteLine(" ");
                        break;
                    case 4:
                        TestRegExp();
                        break;
                }
            }
        }

        public static void TestRegExp()
        {
            RegExp expr1, expr2, expr3, expr4, expr5, a, b, all;
            a = new RegExp("a");
            b = new RegExp("b");
        
            // expr1: "baa"
            expr1 = new RegExp("baa");
            // expr2: "bb"
            expr2 = new RegExp("bb");
            // expr3: "baa | baa"
            expr3 = expr1.or(expr2);
        
            // all: "(a|b)*"
            all = (a.or(b)).star();
        
            // expr4: "(baa | baa)+"
            expr4 = expr3.plus();
            // expr5: "(baa | baa)+ (a|b)*"
            expr5 = expr4.dot(all);

            Console.WriteLine("taal van (baa):\n" + SortedSetToString(expr1.getLanguage(5)));
            Console.WriteLine("taal van (bb):\n" + SortedSetToString(expr2.getLanguage(5)));
            Console.WriteLine("taal van (baa | bb):\n" + SortedSetToString(expr3.getLanguage(5)));
            Console.WriteLine("taal van (a|b)*:\n" + SortedSetToString(all.getLanguage(5)));
            //Console.WriteLine("taal van (baa | bb)+:\n" + SortedSetToString(expr4.getLanguage(5)));
            //Console.WriteLine("taal van (baa | bb)+ (a|b)*:\n" + SortedSetToString(expr5.getLanguage(6)));
        }

        static String SortedSetToString(SortedSet<String> set)
        {
            StringBuilder sb = new StringBuilder();
            foreach (String s in set)
                sb.Append(s + Environment.NewLine);
            return sb.ToString();
        }
    }
}