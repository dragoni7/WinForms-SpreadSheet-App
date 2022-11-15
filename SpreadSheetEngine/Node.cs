// <copyright file="Node.cs" company="Samuel Gibson 011773716">
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
    /// Abstract class for Nodes.
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Evalutes the node.
        /// </summary>
        /// <returns> double result. </returns>
        public abstract double Evaluate();
    }
}
