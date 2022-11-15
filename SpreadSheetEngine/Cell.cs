// <copyright file="Cell.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel;

namespace CptS321
{
    /// <summary>
    /// Abstract Cell class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Text member of the cell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected string text;

        /// <summary>
        /// Value member of the cell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected string value;

        /// <summary>
        /// RowIndex member of the cell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected int rowIndex;

        /// <summary>
        /// ColumnIndex member of the cell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected int columnIndex;

        /// <summary>
        /// backgroundColor member of the cell.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected uint backgroundColor;

        /// <summary>
        /// Whether the cell has default values.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Assignment details these values to be protected")]
        protected bool defaultCell;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row"> cell row. </param>
        /// <param name="column"> cell column. </param>
        public Cell(int row, int column)
        {
            this.rowIndex = row;
            this.columnIndex = column;
            this.backgroundColor = 0xFFFFFFFF;
            this.text = string.Empty;
            this.value = string.Empty;
            this.defaultCell = true;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the RowIndex value.
        /// </summary>
        /// <value>
        /// <int>The RowIndex value.</int>
        /// </value>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// gets the ColumnIndex value.
        /// </summary>
        /// <value>
        /// <int> gets the ColumnIndex value </int>
        /// </value>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets the text value.
        /// </summary>
        /// <value>
        /// <string> The text value.</string>
        /// </value>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value != this.text)
                {
                    this.text = value;
                    this.defaultCell = false;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets the value member.
        /// </summary>
        /// <value>
        /// <string>The value member.</string>
        /// </value>
        public string Value
        {
            get
            {
                if (this.value == null)
                {
                    return string.Empty;
                }
                else
                {
                    return this.value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the backgroundColor member.
        /// </summary>
        /// <value>
        /// <uint>The backgroundColor member.</uint>
        /// </value>
        public uint BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }

            set
            {
                if (value != this.backgroundColor)
                {
                    this.backgroundColor = value;
                    this.defaultCell = false;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("BackgroundColor"));
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the cell is default or not.
        /// </summary>
        /// <value>
        /// <bool>The default value of the cell.</bool>
        /// </value>
        public bool IsDefault
        {
            get
            {
                return this.defaultCell;
            }
        }

        /// <summary>
        /// Clears the contents of the cell back to default.
        /// </summary>
        public void ClearCell()
        {
            this.Text = string.Empty;
            this.value = string.Empty;
            this.backgroundColor = 0xFFFFFFFF;
            this.defaultCell = true;
        }
    }
}
