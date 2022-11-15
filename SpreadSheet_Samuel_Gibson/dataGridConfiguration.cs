// <copyright file="dataGridConfiguration.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CptS321
{
    /// <summary>
    /// Class for configuring the data grid object for WinForms.
    /// </summary>
    public class DataGridConfiguration
    {
        /// <summary>
        /// Builds the columns and rows for a data grid specified by the parameters.
        /// </summary>
        /// <param name="grid"> the data grid. </param>
        /// <param name="headerNames"> String array of header names for columns. </param>
        /// <param name="headerText"> String array of header text for columns. </param>
        /// <param name="rows"> number of rows. </param>
        internal static void BuildColumnsAndRows(DataGridView grid, string[] headerNames, string[] headerText, int rows)
        {
            BuildColumns(grid, headerNames, headerText);
            BuildRows(grid, rows);
        }

        /// <summary>
        /// Constructs columns for a data grid.
        /// </summary>
        /// <param name="grid"> the data grid.</param>
        /// <param name="headerNames"> String array of header names. </param>
        /// <param name="headerText"> String array of header text. </param>
        private static void BuildColumns(DataGridView grid, string[] headerNames, string[] headerText)
        {
            if (headerNames.Length != headerText.Length)
            {
                Console.WriteLine("Each column must have a header name and header text");
            }
            else
            {
                grid.Columns.Clear();
                int length = headerNames.Length;
                for (int i = 0; i < length; i++)
                {
                    grid.Columns.Add(headerNames[i], headerNames[i]);
                }
            }
        }

        /// <summary>
        /// Constructs rows for data grid.
        /// </summary>
        /// <param name="grid"> the data grid. </param>
        /// <param name="rows"> number of rows. </param>
        private static void BuildRows(DataGridView grid, int rows)
        {
            if (rows >= 0)
            {
                grid.Rows.Add(rows);
            }

            for (int i = 0; i < rows; i++)
            {
                grid.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }
    }
}
