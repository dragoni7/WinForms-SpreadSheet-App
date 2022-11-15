// <copyright file="SpreadSheetCell.cs" company="Samuel Gibson 011773716">
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
    /// Class to inherit abstract Cell class.
    /// </summary>
    internal sealed class SpreadSheetCell : Cell
    {
        /// <summary>
        /// Tracks dependencies for SpreadSheetCells.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Only SpreadSheetCell should know about it's dependencies but SpreadSheet needs access to it")]
        internal static Dictionary<SpreadSheetCell, HashSet<SpreadSheetCell>> Dependencies = new Dictionary<SpreadSheetCell, HashSet<SpreadSheetCell>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheetCell"/> class.
        /// Constructor. Derives from Cell constructor.
        /// </summary>
        /// <param name="row"> row number. </param>
        /// <param name="column"> column number. </param>
        public SpreadSheetCell(int row, int column)
            : base(row, column)
        {
        }

        /// <summary>
        /// Sets the value of the cell.
        /// </summary>
        /// <param name="v"> new value. </param>
        public void SetValue(string v)
        {
            this.value = v;
            this.defaultCell = false;
        }
    }
}
