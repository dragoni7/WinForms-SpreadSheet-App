// <copyright file="Program.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// Program to run console app for expression tree.
    /// </summary>
    public class Program
    {
#pragma warning disable SA1600 // Generated code.
        private static void Main(string[] args)
        {
            string input;
            string expression = "A1-12-C1";
            ExpressionTree tree = new ExpressionTree(expression);
            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("MENU    (Current Expression: " + expression + ")");
                Console.WriteLine("     1. Enter new expression");
                Console.WriteLine("     2. Set variable value");
                Console.WriteLine("     3. Evaluate tree");
                Console.WriteLine("     4. Display Variables");
                Console.WriteLine("     5. Quit");

                input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    Console.Write("Enter new expresssion: ");
                    expression = Console.ReadLine();
                    tree = new ExpressionTree(expression);
                }

                if (input.Equals("2"))
                {
                    Console.Write("Enter variable name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter variable value: ");
                    double value;
                    double.TryParse(Console.ReadLine().Trim(), out value);
                    tree.SetVariable(name, value);
                }

                if (input.Equals("3"))
                {
                    Console.WriteLine(tree.Evaluate());
                }

                if (input.Equals("4"))
                {
                    Console.WriteLine();
                    foreach (string variableName in tree.GetVariableNames())
                    {
                        Console.Write(variableName + " ");
                    }

                    Console.WriteLine();
                }
            }
            while (input != "5");
        }
    }
}
