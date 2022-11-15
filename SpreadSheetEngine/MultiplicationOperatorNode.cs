// <copyright file="MultiplicationOperatorNode.cs" company="Samuel Gibson 011773716">
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
    /// Class for multiplication operator node.
    /// </summary>
    internal class MultiplicationOperatorNode : OperatorNode
    {
        /// <summary>
        /// Gets the multiplication operator.
        /// </summary>
        /// <value>
        /// <char>The multiplication operator.</char>
        /// </value>
        public static char Operator => '*';

        /// <summary>
        /// Gets the precedence of the operator.
        /// </summary>
        /// <value>
        /// <ushort>The precedence of the operator.</ushort>
        /// </value>
        public static ushort Precedence => 9;

        /// <summary>
        /// Gets the associativity of the operator.
        /// </summary>
        /// <value>
        /// <Associative>The associativity of the operator.</Associative>
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
        /// Evaluates the multiplication of the left and right node.
        /// </summary>
        /// <returns> double result. </returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
