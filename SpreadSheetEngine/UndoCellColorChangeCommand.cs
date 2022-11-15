// <copyright file="UndoCellColorChangeCommand.cs" company="Samuel Gibson 011773716">
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
    /// Cell background color change command.
    /// </summary>
    public class UndoCellColorChangeCommand : ICommand
    {
        /// <summary>
        /// the color of the cell's background.
        /// </summary>
        private uint color;

        /// <summary>
        /// the target cell.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoCellColorChangeCommand"/> class.
        /// </summary>
        /// <param name="newColor"> color to update to. </param>
        /// <param name="newCell"> cell to be updated. </param>
        public UndoCellColorChangeCommand(uint newColor, Cell newCell)
        {
            this.color = newColor;
            this.cell = newCell;
        }

        /// <inheritdoc/>
        public ICommand Execute(SpreadSheet spreadSheet)
        {
            uint temp = this.cell.BackgroundColor;
            this.cell.BackgroundColor = this.color;
            return new UndoCellColorChangeCommand(temp, this.cell);
        }
    }
}
