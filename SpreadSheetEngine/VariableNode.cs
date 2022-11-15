// <copyright file="VariableNode.cs" company="Samuel Gibson 011773716">
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
    /// Node to hold a variable.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// Dictionary to store double value for string variables in expressions.
        /// </summary>
#pragma warning disable SA1401 // Variables needs to be available to ExpressionTree for setVariable()
        internal static Dictionary<string, double> Variables = new Dictionary<string, double>();

        /// <summary>
        /// Gets or sets name of VariableNode.
        /// </summary>
        /// <value>
        /// <string>Name of VariableNode.</string>
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Evaluates the value of the string variable.
        /// </summary>
        /// <returns> double result. </returns>
        public override double Evaluate()
        {
            double value = 0.0;
            if (Variables.TryGetValue(this.Name, out value))
            {
                return value;
            }
            else
            {
                return value;
            }
        }
    }
}
