// <copyright file="UndoRedoCollection.cs" company="Samuel Gibson 011773716">
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
    /// Class to store undo redo commands and associated information.
    /// </summary>
    public class UndoRedoCollection
    {
        /// <summary>
        /// collection title.
        /// </summary>
        private string title;

        /// <summary>
        /// collection commands.
        /// </summary>
        private List<ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRedoCollection"/> class.
        /// </summary>
        /// <param name="cmds"> the commands to add to the collection.</param>
        /// <param name="newTitle"> the title for the collection.</param>
        public UndoRedoCollection(List<ICommand> cmds, string newTitle)
        {
            this.commands = cmds;
            this.Title = newTitle;
        }

        /// <summary>
        /// Gets or sets the collection title.
        /// </summary>
        /// <value>
        /// <string> The collection title.</string>
        /// </value>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Executes each command in collection.
        /// </summary>
        /// <param name="spreadSheet"> the active spreadsheet. </param>
        /// <returns> new collection of executed commands.</returns>
        public UndoRedoCollection Execute(SpreadSheet spreadSheet)
        {
            List<ICommand> cmds = new List<ICommand>();

            foreach (ICommand cmd in this.commands)
            {
                cmds.Add(cmd.Execute(spreadSheet));
            }

            return new UndoRedoCollection(cmds, this.Title);
        }
    }
}
