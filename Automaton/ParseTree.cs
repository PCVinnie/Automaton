using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
     class ParseTree 
     { 
         public enum NodeType 
         { 
             CHAR, 
             STAR, 
             QUESTION, 
             PLUS,
             ALTER, 
             CONCAT 
         } 
         public NodeType type; 
         public char data; 
         public ParseTree left;
         public ParseTree right; 
         public ParseTree(NodeType t, char d, ParseTree l, ParseTree r) 
         { 
             type = t; 
             data = d; 
             left = l; 
             right = r;
         } 
     } 
}
