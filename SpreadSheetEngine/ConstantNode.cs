// <copyright file="ConstantNode.cs" company="Samuel Gibson 011773716">
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
    /// Node to hold constant values.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// Gets or sets value of the ConstantNode.
        /// </summary>
        /// <value>
        /// <double>Value of the ConstantNode.</double>
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Returns the value of the ConstantNode.
        /// </summary>
        /// <returns> double value. </returns>
        public override double Evaluate()
        {
            return this.Value;
        }
    }
}
