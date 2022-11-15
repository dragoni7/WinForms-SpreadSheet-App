// <copyright file="ParenthesisMarker.cs" company="Samuel Gibson 011773716">
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
    /// OperatorNode to mark the beginning of a parenthesis.
    /// </summary>
    internal class ParenthesisMarker : OperatorNode
    {
        /// <summary>
        /// Gets the precedence of the marker.
        /// </summary>
        /// <value>
        /// <ushort>The precedence of the marker.</ushort>
        /// </value>
        public override ushort GetPrecedence => 0;

        /// <summary>
        /// Inherited evalute method.
        /// </summary>
        /// <returns> returns 0.</returns>
        public override double Evaluate()
        {
            return 0.0;
        }
    }
}
