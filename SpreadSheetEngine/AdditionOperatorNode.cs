// <copyright file="AdditionOperatorNode.cs" company="Samuel Gibson 011773716">
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
    /// Class for addition operator node.
    /// </summary>
    internal class AdditionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the addition operator.
        /// </summary>
        /// <value>
        /// <char>The addition operator.</char>
        /// </value>
        public static char Operator => '+';

        /// <summary>
        /// Gets the precedence of the operator.
        /// </summary>
        /// <value>
        /// <ushort>The precedence of the operator.</ushort>
        /// </value>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets the associativity of the node.
        /// </summary>
        /// <value>
        /// <Associative>The associativity of the node.</Associative>
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
        /// Evaluates the sum of the left and right node.
        /// </summary>
        /// <returns> double value. </returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
