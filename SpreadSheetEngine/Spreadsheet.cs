// <copyright file="SpreadSheet.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CptS321
{
    /// <summary>
    /// SpreadSheet class to build cell array.
    /// </summary>
    public class SpreadSheet
    {
        /// <summary>
        /// Dictionary for column header.
        /// </summary>
        private readonly Dictionary<char, int> columnHeaderRef = new Dictionary<char, int>() { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 }, { 'E', 4 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }, { 'I', 9 }, { 'J', 10 }, { 'K', 11 }, { 'L', 12 }, { 'M', 13 }, { 'N', 14 }, { 'O', 15 }, { 'P', 16 }, { 'Q', 17 }, { 'R', 18 }, { 'S', 19 }, { 'T', 20 }, { 'U', 21 }, { 'V', 22 }, { 'W', 23 }, { 'X', 24 }, { 'Y', 25 }, { 'Z', 26 } };

        /// <summary>
        /// amount of rows.
        /// </summary>
        private int rowCount = 0;

        /// <summary>
        /// amount of columns.
        /// </summary>
        private int columnCount = 0;

        /// <summary>
        /// the 2D array of cells.
        /// </summary>
        private SpreadSheetCell[,] cellArray;

        /// <summary>
        /// The undo and redos for the spreadsheet.
        /// </summary>
        private UndoRedoHandler spreadSheetUndoRedos = new UndoRedoHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheet"/> class.
        /// </summary>
        /// <param name="rows"> amount of rows. </param>
        /// <param name="columns"> amount of columns. </param>
        public SpreadSheet(int rows, int columns)
        {
            this.cellArray = new SpreadSheetCell[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    SpreadSheetCell c = new SpreadSheetCell(i, j);
                    c.PropertyChanged += this.CellPropertyChanged;
                    this.cellArray[i, j] = c;
                }
            }

            this.rowCount = this.RowCount;
            this.columnCount = this.ColumnCount;
        }

        /// <summary>
        /// Event for gathering cell value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>
        /// <int> The number of columns.</int>
        /// </value>
        public int ColumnCount
        {
            get { return this.cellArray.GetLength(1); }
        }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// <int> The number of rows.</int>
        /// </value>
        public int RowCount
        {
            get { return this.cellArray.GetLength(0); }
        }

        /// <summary>
        /// Gets the spreadsheet's undos and redo handler.
        /// </summary>
        /// <value>
        /// <UndoRedoHandler>The spreadsheet's undos and redo handler.</UndoRedoHandler>
        /// </value>
        public UndoRedoHandler UndoRedos
        {
            get { return this.spreadSheetUndoRedos; }
        }

        /// <summary>
        /// Calls the ClearCell method on all non-default cells in the spreadsheet.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < this.cellArray.GetLength(1); j++)
                {
                    SpreadSheetCell cell = this.cellArray[i, j];
                    if (!cell.IsDefault)
                    {
                        this.cellArray[i, j].ClearCell();
                    }
                }
            }
        }

        /// <summary>
        /// Writes the data for the spreadsheet to an XmlWriter.
        /// </summary>
        /// <param name="writer"> the passed XmlWriter.</param>
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement("Spreadsheet");
            foreach (Cell c in this.cellArray)
            {
                if (!c.IsDefault)
                {
                    writer.WriteStartElement("Cell");
                    writer.WriteAttributeString("ColumnIndex", c.ColumnIndex.ToString());
                    writer.WriteAttributeString("RowIndex", c.RowIndex.ToString());
                    writer.WriteElementString("Text", c.Text);
                    writer.WriteElementString("BackgroundColor", c.BackgroundColor.ToString());
                    writer.WriteEndElement();
                }
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Loads data from an xml file to the spread sheet.
        /// </summary>
        /// <param name="element"> the root of the xml file.</param>
        public void Load(XElement element)
        {
            Cell currentCell;
            string newText;
            uint newColor;

            this.spreadSheetUndoRedos.Clear();

            foreach (XElement c in element.Elements("Cell"))
            {
                currentCell = this.GetCell(int.Parse(c.Attribute("RowIndex").Value), int.Parse(c.Attribute("ColumnIndex").Value));
                if (currentCell != null)
                {
                    newText = c.Element("Text").Value;

                    if (newText != null)
                    {
                        currentCell.Text = newText;
                    }

                    newColor = uint.Parse(c.Element("BackgroundColor").Value);
                    currentCell.BackgroundColor = newColor;
                }
            }
        }

        /// <summary>
        /// Adds a new undo command collection to the spreadsheets's undo redo handler.
        /// </summary>
        /// <param name="commands"> the command collection.</param>
        /// <param name="title"> the collection title.</param>
        public void AddUndo(List<ICommand> commands, string title)
        {
            this.spreadSheetUndoRedos.AddUndo(new UndoRedoCollection(commands, title));
        }

        /// <summary>
        /// gets the indicated cell.
        /// </summary>
        /// <param name="row"> cell row.</param>
        /// <param name="column"> cell column.</param>
        /// <returns> the cell.</returns>
        public Cell GetCell(int row, int column)
        {
            Cell cell;
            try
            {
                cell = this.cellArray[row, column];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }

            return cell;
        }

        /// <summary>
        /// gets the indicated cell.
        /// </summary>
        /// <param name="location"> text field of the cell.</param>
        /// <returns> the cell. </returns>
        public Cell GetCell(string location)
        {
            if (char.IsLetter(location[0]))
            {
                int row;
                if (int.TryParse(location.Substring(1), out row))
                {
                    return this.GetCell(row - 1, this.columnHeaderRef[location[0]]);
                }
            }

            return null;
        }

        /// <summary>
        /// sets the value of the cell.
        /// </summary>
        /// <param name="sender"> cell. </param>
        /// <param name="e"> Property that changed. </param>
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SpreadSheetCell cell = sender as SpreadSheetCell;

            if (e.PropertyName.Equals("Text"))
            {
                if (cell.Text.Equals(string.Empty))
                {
                    cell.SetValue(string.Empty);
                    SpreadSheetCell.Dependencies.Remove(cell);
                    this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
                    return;
                }

                // Set dependencies.
                else if (cell.Text[0].Equals('='))
                {
                    ExpressionTree equation = new ExpressionTree(cell.Text.Substring(1));

                    foreach (string variable in equation.GetVariableNames())
                    {
                        if (this.GetCell(variable) == null)
                        {
                            continue;
                        }
                        else if (!SpreadSheetCell.Dependencies.ContainsKey(this.GetCell(variable) as SpreadSheetCell))
                        {
                            SpreadSheetCell.Dependencies[this.GetCell(variable) as SpreadSheetCell] = new HashSet<SpreadSheetCell>();
                        }

                        SpreadSheetCell.Dependencies[this.GetCell(variable) as SpreadSheetCell].Add(cell);
                    }
                }

                this.Evaluate(cell);
            }
            else if (e.PropertyName.Equals("BackgroundColor"))
            {
                this.PropertyChanged(cell, new PropertyChangedEventArgs("BackgroundColor"));
            }
        }

        /// <summary>
        /// Evaluates a cell along with it's dependencies if they exist.
        /// </summary>
        /// <param name="cell"> the cell to evaluate.</param>
        private void Evaluate(SpreadSheetCell cell)
        {
            if (cell.Text.Equals(string.Empty))
            {
                cell.SetValue(string.Empty);
                this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
            }
            else if (cell.Text[0].Equals('='))
            {
                ExpressionTree equation = new ExpressionTree(cell.Text.Substring(1));

                foreach (string variableName in equation.GetVariableNames())
                {
                    // if self reference.
                    if (this.GetCell(variableName) == cell)
                    {
                        cell.SetValue("!(self reference)");
                        this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
                        return;
                    }
                    else if (!this.SetEquationVariable(equation, variableName) && equation.GetVariableNames().Count == 1)
                    {
                        Cell targetCell = this.GetCell(variableName);

                        // bad reference.
                        if (targetCell == null)
                        {
                            cell.SetValue("!(bad reference)");
                        }
                        else
                        {
                            cell.SetValue(targetCell.Text);
                        }

                        this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
                        return;
                    }
                    else if (!this.SetEquationVariable(equation, variableName))
                    {
                        cell.SetValue(cell.Text);
                        this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
                        return;
                    }

                    // circular ref
                    else if (this.HasCircularRef(this.GetCell(variableName) as SpreadSheetCell, cell))
                    {
                        cell.SetValue("!(circular reference)");
                        this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
                        return;
                    }
                }

                cell.SetValue(equation.Evaluate().ToString());
                this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
            }
            else
            {
                cell.SetValue(cell.Text);
                this.PropertyChanged(cell, new PropertyChangedEventArgs("Value"));
            }

            if (SpreadSheetCell.Dependencies.ContainsKey(cell))
            {
                foreach (SpreadSheetCell variable in SpreadSheetCell.Dependencies[cell])
                {
                    this.Evaluate(variable);
                }
            }
        }

        /// <summary>
        /// Sets variables in a ExpressionTree. Defaults to 0.
        /// </summary>
        /// <param name="equation"> the equation to change variables. </param>
        /// <param name="variable"> the variable to set a value to.</param>
        /// <returns> success status. </returns>
        private bool SetEquationVariable(ExpressionTree equation, string variable)
        {
            Cell cell = this.GetCell(variable);

            double number;

            if (cell == null)
            {
                return false; // return false if cell doesn't exist. The variable is not valid.
            }
            else if (double.TryParse(cell.Value, out number))
            {
                equation.SetVariable(variable, number);
                return true; // success
            }
            else
            {
                equation.SetVariable(variable, 0);
                return true; // return default value, 0.
            }
        }

        /// <summary>
        /// Checks if the cell contains a circular reference.
        /// </summary>
        /// <param name="baseCell"> the base cell to check for.</param>
        /// <param name="depnCell"> the dependant cell to check.</param>
        /// <returns> true or false depending on if circular reference exists.</returns>
        private bool HasCircularRef(SpreadSheetCell baseCell, SpreadSheetCell depnCell)
        {
            if (!SpreadSheetCell.Dependencies.ContainsKey(depnCell))
            {
                return false;
            }

            if (baseCell == depnCell)
            {
                return true;
            }

            foreach (SpreadSheetCell entry in SpreadSheetCell.Dependencies[depnCell])
            {
                return this.HasCircularRef(baseCell, entry);
            }

            return false;
        }
    }
}
