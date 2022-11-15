// <copyright file="ICommand.cs" company="Samuel Gibson 011773716">
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
    /// Interface for creating ICommand instances.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the command on the spreadsheet.
        /// </summary>
        /// <param name="spreadSheet"> the active spreadsheet.</param>
        /// <returns> new instance of the commmand for redo.</returns>
        ICommand Execute(SpreadSheet spreadSheet);
    }
}
