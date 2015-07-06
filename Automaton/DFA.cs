using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automaton
{
    class DFA : Representation
    {
        string[,] matrix;
        char[] symbols;

        public DFA(string[,] matrix, char[] symbols)
        {
            this.matrix = matrix;
            this.symbols = symbols;
        }

        public void printDFA()
        {
            base.print();
            string symbol = "";
            string table = "";
            string space = "";
            bool asPrint = true;
            int totLength = 0;
            int strLength = 0;

            //Schaalt het tabel in de console.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {

                for (int j = 0; j < symbols.Length; j++)
                {

                    for (int l = 0; l < matrix.GetLength(0); l++)
                    {
                        if (matrix[l, 0].Length > strLength)
                        {
                            strLength = matrix[l, 0].Length;
                        }
                    }

                    totLength = strLength - matrix[i, j].Length;

                    for (int m = 0; m < totLength; m++)
                    {
                        space += " ";
                    }

                    if (matrix[i, j + 1] == null)
                    {
                        table += space + " |  " + matrix[i, j + 1];
                    }
                    else
                    {
                        table += space + " | " + matrix[i, j + 1];
                    }
                    space = "";

                }

                if (asPrint == true)
                {

                    totLength = strLength - 1;

                    for (int k = 0; k < totLength; k++)
                    {
                        space += " ";
                    }

                    for (int l = 0; l < symbols.Length; l++)
                    {
                        symbol += space + " | " + symbols[l];
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" " + symbol);
                    Console.ResetColor();

                    symbol = "";
                    space = "";
                    asPrint = false;

                }
                Console.WriteLine(matrix[i, 0] + table);
                table = "";
            }

            Console.WriteLine(" ");
            writeFileDFA("dfa.txt");
        
        }

        public void writeFileDFA(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                string txt = "";
                for (int symbol = 0; symbol < symbols.Length; symbol++)
                {
                    txt += "   " + symbols[symbol];
                }
                sw.WriteLine(txt);

                string columns = "";
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < symbols.Length; j++)
                    {
                        columns += " " + matrix[i, j + 1];
                    }
                    sw.WriteLine(matrix[i, 0] + " " + columns);
                    columns = "";
                }

                Console.WriteLine("DFA is opgeslagen in " + path);
            }
        }
    }
}
