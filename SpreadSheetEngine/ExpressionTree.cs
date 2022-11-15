// <copyright file="ExpressionTree.cs" company="Samuel Gibson 011773716">
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
    /// Class for creating an expression tree.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// root of expression tree.
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> the expression to create the tree with. </param>
        public ExpressionTree(string expression)
        {
            VariableNode.Variables.Clear();
            this.root = this.BuildPostFixTree(expression.Trim());
        }

        /// <summary>
        /// Sets a variable in an expression with a double value.
        /// </summary>
        /// <param name="variableName"> variable in expression to change. </param>
        /// <param name="variableValue"> value to associate the variable with. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            VariableNode.Variables[variableName] = variableValue;
        }

        /// <summary>
        /// Calculates the expression.
        /// </summary>
        /// <returns> a double value result.</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Gets a list of all variable names.
        /// </summary>
        /// <returns> List of variable names. </returns>
        public List<string> GetVariableNames()
        {
            return VariableNode.Variables.Keys.ToList();
        }

        /// <summary>
        /// Builds a tree in postfix form while accounting for operator precedence and parenthesis.
        /// </summary>
        /// <param name="expression"> the expression to build the tree from. </param>
        /// <returns> the root of the tree.</returns>
        private Node BuildPostFixTree(string expression)
        {
            OperatorNodeFactory factory = new OperatorNodeFactory();
            Stack<OperatorNode> operatorStack = new Stack<OperatorNode>();
            Stack<Node> nodeStack = new Stack<Node>();

            for (int index = 0; index <= expression.Length - 1; index++)
            {
                OperatorNode operatorNode = factory.CreateOperatorNode(expression[index]);

                // If an operatorNode was able to be created
                if (operatorNode != null)
                {
                    if (operatorStack.Count() == 0)
                    {
                        operatorStack.Push(operatorNode);
                    }

                    // If the current operator has smaller precedence then the operator on the top of the stack, pop the top and push it onto the node stack before pushing the current operator on the operator stack.
                    else if (operatorStack.Peek().GetPrecedence > operatorNode.GetPrecedence)
                    {
                        OperatorNode temp = operatorStack.Pop();
                        temp.Right = nodeStack.Pop();
                        temp.Left = nodeStack.Pop();
                        nodeStack.Push(temp); // nodeStack now contains an operator node with a properly set left and right nodes.
                        operatorStack.Push(operatorNode);
                    }

                    // Otherwise the current operator will just push onto the stack.
                    else
                    {
                        operatorStack.Push(operatorNode);
                    }
                }

                // If an operator node was unable to be created, it must either be a variable, constant, or parenthesis.
                else
                {
                    double number;

                    // push the parenthesis marker onto the operator stack.
                    if (expression[index] == '(')
                    {
                        ParenthesisMarker p = new ParenthesisMarker();
                        operatorStack.Push(p);
                    }

                    // When we reach the right parenthesis, discard it, and beging popping values off until the parenthesis marker is found. Parenthesis maker has precedence of 0
                    else if (expression[index] == ')')
                    {
                        while (operatorStack.Peek().GetPrecedence != 0)
                        {
                            OperatorNode temp = operatorStack.Pop();
                            temp.Right = nodeStack.Pop();
                            temp.Left = nodeStack.Pop();
                            nodeStack.Push(temp);
                        }

                        operatorStack.Pop();
                    }
                    else
                    {
                        string value = this.DetermineValue(expression, ref index); // Determines if a constant or variable is multiple chars long.

                        if (double.TryParse(value, out number))
                        {
                            ConstantNode c = new ConstantNode();
                            c.Value = number;
                            nodeStack.Push(c);
                        }
                        else
                        {
                            VariableNode.Variables[value] = 0.0;
                            VariableNode v = new VariableNode();
                            v.Name = value;
                            nodeStack.Push(v);
                        }
                    }
                }
            }

            // At the end, pop off all operators
            while (operatorStack.Count > 0)
            {
                OperatorNode temp = operatorStack.Pop();
                temp.Right = nodeStack.Pop();
                temp.Left = nodeStack.Pop();
                nodeStack.Push(temp);
            }

            return nodeStack.Pop();
        }

        /// <summary>
        /// Determines a value from an expression at a starting index.
        /// </summary>
        /// <param name="expression"> the expression to search. </param>
        /// <param name="index"> the starting index of the expression. </param>
        /// <returns> a string value representing either a variable or a double constant. </returns>
        private string DetermineValue(string expression, ref int index)
        {
            StringBuilder sb = new StringBuilder();

            OperatorNodeFactory factory = new OperatorNodeFactory();
            for (; index <= expression.Length - 1; index++)
            {
                 if (factory.CreateOperatorNode(expression[index]) == null && expression[index] != '(' && expression[index] != ')')
                {
                    sb.Append(expression[index]);
                }
                 else
                {
                    index -= 1;
                    break;
                }
            }

            return sb.ToString();
        }
    }
}
