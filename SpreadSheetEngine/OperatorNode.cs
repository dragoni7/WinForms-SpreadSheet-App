// <copyright file="OperatorNode.cs" company="Samuel Gibson 011773716">
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
    /// Base OperatorNode.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// Associativity for operators.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// Right associative.
            /// </summary>
            Right,

            /// <summary>
            /// Left associative.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets precedence of the operator.
        /// </summary>
        /// <value>
        /// <ushort>Precedence of the operator.</ushort>
        /// </value>
        public abstract ushort GetPrecedence { get; }

        /// <summary>
        /// Gets or sets Left node.
        /// </summary>
        /// <value>
        /// <Node>Left node.</Node>
        /// </value>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets Right node.
        /// </summary>
        /// <value>
        /// <Node>Right node.</Node>
        /// </value>
        public Node Right { get; set; }
    }
}
