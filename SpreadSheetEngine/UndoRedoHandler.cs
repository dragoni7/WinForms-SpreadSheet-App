// <copyright file="UndoRedoHandler.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// Class to handle Undo and Redo commands for a spread sheet.
    /// </summary>
    public class UndoRedoHandler
    {
        /// <summary>
        /// Stack of undo collections.
        /// </summary>
        private Stack<UndoRedoCollection> undos = new Stack<UndoRedoCollection>();

        /// <summary>
        /// Stack of redo collections.
        /// </summary>
        private Stack<UndoRedoCollection> redos = new Stack<UndoRedoCollection>();

        /// <summary>
        /// Amount of Undo Collections in stack.
        /// </summary>
        /// <returns> int count. </returns>
        public int Undos()
        {
            return this.undos.Count();
        }

        /// <summary>
        /// Amount of Redo Collections in stack.
        /// </summary>
        /// <returns> int count. </returns>
        public int Redos()
        {
            return this.redos.Count();
        }

        /// <summary>
        /// Gets the title of the top item in the Undo Collection stack.
        /// </summary>
        /// <returns> title.</returns>
        public string GetUndoTitle()
        {
            if (this.Undos() > 0)
            {
                return this.undos.Peek().Title;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the title of the top item in the Redo Collection stack.
        /// </summary>
        /// <returns> title.</returns>
        public string GetRedoTitle()
        {
            if (this.Redos() > 0)
            {
                return this.redos.Peek().Title;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Adds a undo collection to the Undo Collection stack.
        /// </summary>
        /// <param name="undo"> new undo collection. </param>
        public void AddUndo(UndoRedoCollection undo)
        {
            this.undos.Push(undo);
            this.redos.Clear();
        }

        /// <summary>
        /// Executes the Undo Collection.
        /// </summary>
        /// <param name="spreadSheet"> the active spreadsheet.</param>
        public void Undo(SpreadSheet spreadSheet)
        {
            UndoRedoCollection undoCommands = this.undos.Pop();
            this.redos.Push(undoCommands.Execute(spreadSheet));
        }

        /// <summary>
        /// Executes the Redo Collection.
        /// </summary>
        /// <param name="spreadSheet"> the active spreadsheet.</param>
        public void Redo(SpreadSheet spreadSheet)
        {
            UndoRedoCollection redoCommands = this.redos.Pop();
            this.undos.Push(redoCommands.Execute(spreadSheet));
        }

        /// <summary>
        /// Clears the undo redo stacks.
        /// </summary>
        public void Clear()
        {
            this.undos.Clear();
            this.redos.Clear();
        }
    }
}
