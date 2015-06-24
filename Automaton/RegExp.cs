using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class RegExp : Representation
    {
        public enum Operator { PLUS, STAR, OR, DOT, ONE }
        Operator op;
        String terminals;
        RegExp left, right;

        public RegExp()
        {
            op = Operator.ONE;
            terminals = "";
            left = null;
            right = null;
        }
    
        public RegExp(String p)
        {
            op = Operator.ONE;
            terminals = p;
            left = null;
            right = null;
        }
    
        public RegExp plus()
        {
            RegExp result = new RegExp();
            result.op = Operator.PLUS;
            result.left = this;
            return result;
        }

        public RegExp star()
        {
            RegExp result = new RegExp();
            result.op = Operator.STAR;
            result.left = this;
            return result;
        }

        public RegExp or(RegExp e2)
        {
            RegExp result = new RegExp();
            result.op = Operator.OR;
            result.left = this;
            result.right = e2;
            return result;
        }

        public RegExp dot(RegExp e2)
        {
            RegExp result = new RegExp();
            result.op = Operator.DOT;
            result.left = this;
            result.right = e2;
            return result;
        }

        public SortedSet <String> getLanguage(int maxSteps)
        {
            SortedSet<String> emptyLanguage = new SortedSet<String>();
            SortedSet<String> languageResult = new SortedSet<String>();
        
            SortedSet<String> languageLeft, languageRight;
        
            if (maxSteps < 1) 
                return emptyLanguage;
        
            switch (this.op) 
            {
                case Operator.ONE:
                    languageResult.Add(terminals);
                    break;

                case Operator.OR:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                    foreach (String s in languageLeft)
                        languageResult.Add(s);
                    foreach (String s in languageRight)
                        languageResult.Add(s);
                    break;

                case Operator.DOT:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    languageRight = right == null ? emptyLanguage : right.getLanguage(maxSteps - 1);
                    foreach (String s1 in languageLeft)
                        foreach (String s2 in languageRight)
                           languageResult.Add(s1 + s2);
                    break;

                case Operator.STAR:

                case Operator.PLUS:
                    languageLeft = left == null ? emptyLanguage : left.getLanguage(maxSteps - 1);
                    foreach (String s in languageLeft)
                        languageResult.Add(s);
                    for (int i = 1; i < maxSteps; i++)
                    {   
                        HashSet<String> languageTemp = new HashSet<String>(languageResult);
                        foreach (String s1 in languageLeft)
                            foreach (String s2 in languageTemp)
                               languageResult.Add(s1 + s2);
                    }
                    if (this.op  == Operator.STAR)
                        languageResult.Add("");
                    break;
                
                default:
                    Console.WriteLine("getLanguage is nog niet gedefinieerd voor de operator: " + this.op);
                    break;
            }
            return languageResult;
        }    
       
        public void printRegExp()
        {
            Console.WriteLine("");
        }

        public void ToNDFA(string input)
        {

        }

        public static bool CheckRegExpInput(string input)
        {
            char[] regExpChars = "()ab|*".ToCharArray();
            foreach (char c in input.Where(c => !regExpChars.Contains(c)))
                return false;
            return true;
        }
    }
}