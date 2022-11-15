// <copyright file="Form1.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CptS321
{
#pragma warning disable SA1601 // Generated class.
    public partial class Form1 : Form
    {
        /// <summary>
        /// Header for columns.
        /// </summary>
        private readonly string[] columnHeader = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <summary>
        /// The spreadSheet object for the form.
        /// </summary>
        private SpreadSheet spreadSheet;

#pragma warning disable SA1600 // Part of generated class.
        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridConfiguration.BuildColumnsAndRows(this.dataGridView1, this.columnHeader, this.columnHeader, 50);
            this.spreadSheet = new SpreadSheet(50, 26);
            this.dataGridView1.CellBeginEdit += this.DataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit += this.DataGridView1_CellEndEdit;
            this.spreadSheet.PropertyChanged += this.SpreadSheetPropertyChanged;

            ToolStripMenuItem group = this.menuStrip1.Items[1] as ToolStripMenuItem;
            ToolStripItem item = group.DropDownItems[0];
            item.Enabled = false;
            item = group.DropDownItems[1];
            item.Enabled = false;
        }

        private void SpreadSheetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;

            if (e.PropertyName.Equals("Value"))
            {
                if (cell != null)
                {
                    this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
                }
            }
            else if (e.PropertyName.Equals("BackgroundColor"))
            {
                if (cell != null)
                {
                    this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb((int)cell.BackgroundColor);
                }
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event.
        /// </summary>
        /// <param name="sender"> the changing cell. </param>
        /// <param name="e"> spreadsheet argument. </param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.spreadSheet.GetCell(e.RowIndex, e.ColumnIndex).Text;
        }

        /// <summary>
        /// Handles the CellEndEdit event.
        /// </summary>
        /// <param name="sender"> the changing cell. </param>
        /// <param name="e"> spreadsheet argument. </param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Cell cell = this.spreadSheet.GetCell(e.RowIndex, e.ColumnIndex);

            List<ICommand> undos = new List<ICommand>();
            undos.Add(new UndoCellTextChangeCommand(cell.Text, cell));

            string newText;

            if (this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                newText = string.Empty;
            }
            else
            {
                newText = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            cell.Text = newText;

            this.spreadSheet.AddUndo(undos, "Changing Cell Text...");
            this.SetUndoRedoStatus();

            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cell.Value;
        }

        private void DemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string demoText = "abc";
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows[rand.Next(0, 50)].Cells[rand.Next(1, 26)].Value = demoText;
            }

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows[i].Cells[1].Value = "This is cell B #" + (i + 1);
                this.dataGridView1.Rows[i].Cells[2].Value = i + 1;
            }

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows[i].Cells[0].Value = "=B" + (i + 1).ToString();
                this.dataGridView1.Rows[i].Cells[3].Value = "=C" + (i + 1).ToString() + "+5";
            }

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows[i].Cells[4].Value = "=C" + (i + 1).ToString() + "+D" + (i + 1).ToString();
            }
        }

        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            List<ICommand> undos = new List<ICommand>();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                uint newColor = (uint)colorDialog.Color.ToArgb();

                foreach (DataGridViewCell cell in this.dataGridView1.SelectedCells)
                {
                    undos.Add(new UndoCellColorChangeCommand(this.spreadSheet.GetCell(cell.RowIndex, cell.ColumnIndex).BackgroundColor, this.spreadSheet.GetCell(cell.RowIndex, cell.ColumnIndex)));
                    this.spreadSheet.GetCell(cell.RowIndex, cell.ColumnIndex).BackgroundColor = newColor;
                }

                this.spreadSheet.AddUndo(undos, "Changing Cell Color...");
                this.SetUndoRedoStatus();
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheet.UndoRedos.Undo(this.spreadSheet);
            this.SetUndoRedoStatus();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheet.UndoRedos.Redo(this.spreadSheet);
            this.SetUndoRedoStatus();
        }

        /// <summary>
        /// Sets the name and status of the Undo and Redo buttons in the form.
        /// </summary>
        private void SetUndoRedoStatus()
        {
            ToolStripMenuItem group = this.menuStrip1.Items[1] as ToolStripMenuItem;
            ToolStripItem item = group.DropDownItems[0]; // Undo
            item.Enabled = this.spreadSheet.UndoRedos.Undos() > 0;
            item.Text = "Undo " + this.spreadSheet.UndoRedos.GetUndoTitle();

            item = group.DropDownItems[1]; // Redo
            item.Enabled = this.spreadSheet.UndoRedos.Redos() > 0;
            item.Text = "Redo " + this.spreadSheet.UndoRedos.GetRedoTitle();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "*XML Files (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.NewLineChars = "\r\n";
                settings.NewLineOnAttributes = false;
                settings.Indent = true;

                XmlWriter writer = XmlWriter.Create(stream, settings);

                if (writer != null)
                {
                    this.spreadSheet.Save(writer);
                    writer.Close();
                }

                stream.Dispose();
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*XML Files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                XDocument document = null;

                try
                {
                    document = XDocument.Load(stream);
                }
                catch (XmlException)
                {
                    return;
                }

                this.spreadSheet.Clear();
                XElement root = document.Root;
                this.spreadSheet.Load(root);

                stream.Dispose();
            }
        }
    }
}
