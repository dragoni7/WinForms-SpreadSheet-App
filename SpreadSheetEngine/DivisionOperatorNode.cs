// <copyright file="DivisionOperatorNode.cs" company="Samuel Gibson 011773716">
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
    /// Class for division operator node.
    /// </summary>
    internal class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the division operator.
        /// </summary>
        /// <value>
        /// <char>The division operator.</char>
        /// </value>
        public static char Operator => '/';

        /// <summary>
        /// Gets the precedence of the operator.
        /// </summary>
        /// <value>
        /// <ushort>The precedence of the operator.</ushort>
        /// </value>
        public static ushort Precedence => 8;

        /// <summary>
        /// Gets the associativity of the node.
        /// </summary>
        /// <value>
        /// <Associative>The associativity of the node </Associative>
        /// </value>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets the precedence of the operator.
        /// </summary>
        /// <value>
        /// <ushort>The precedence of the operator.</ushort>
        /// </value>
        public override ushort GetPrecedence
        {
            get { return Precedence; }
        }

        /// <summary>
        /// Divides the left node by the right node.
        /// </summary>
        /// <returns> double result. </returns>
        public override double Evaluate()
        {
            double leftNum = this.Left.Evaluate();
            double rightNum = this.Right.Evaluate();

            if (rightNum == 0 || rightNum == 0.0)
            {
                throw new DivideByZeroException("Cannot divide " + leftNum + " by 0");
            }
            else
            {
                return leftNum / rightNum;
            }
        }
    }
}
