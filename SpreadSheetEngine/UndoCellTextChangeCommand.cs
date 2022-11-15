// <copyright file="UndoCellTextChangeCommand.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

namespace CptS321
{
    /// <summary>
    /// Command for cell text change.
    /// </summary>
    public class UndoCellTextChangeCommand : ICommand
    {
        /// <summary>
        /// the text of the cell.
        /// </summary>
        private string text;

        /// <summary>
        /// the target cell.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoCellTextChangeCommand"/> class.
        /// </summary>
        /// <param name="newText"> text to update to. </param>
        /// <param name="newCell"> the cell to be updated. </param>
        public UndoCellTextChangeCommand(string newText, Cell newCell)
        {
            this.text = newText;
            this.cell = newCell;
        }

        /// <inheritdoc/>
        public ICommand Execute(SpreadSheet spreadSheet)
        {
            string temp = this.cell.Text;
            this.cell.Text = this.text;
            return new UndoCellTextChangeCommand(temp, this.cell);
        }
    }
}
