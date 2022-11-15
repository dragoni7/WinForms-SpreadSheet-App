// <copyright file="UnitTest1.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CptS321
{
    /// <summary>
    /// Tests for the SpreadSheet project.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests for the ExpressionTree class.
        /// </summary>
        [TestMethod]
        public void ExpressionTreeTests()
        {
            // Test Evalute().
            ExpressionTree expTree = new ExpressionTree("5+5");
            Assert.AreEqual(expTree.Evaluate(), 10);
            expTree = new ExpressionTree("1.3+1.3");
            Assert.AreEqual(expTree.Evaluate(), 2.6);
            expTree = new ExpressionTree("0-1.5");
            Assert.AreEqual(expTree.Evaluate(), -1.5);
            expTree = new ExpressionTree("1*2.5");
            Assert.AreEqual(expTree.Evaluate(), 2.5);
            expTree = new ExpressionTree("5/2");
            Assert.AreEqual(expTree.Evaluate(), 2.5);

            // Test divide by zero.
            expTree = new ExpressionTree("5/0");
            Assert.ThrowsException<DivideByZeroException>(() => expTree.Evaluate());

            // Test SetVariable().
            expTree = new ExpressionTree("A+5");
            expTree.SetVariable("A", 2.5);
            Assert.AreEqual(expTree.Evaluate(), 7.5);

            // Test non set variable
            expTree = new ExpressionTree("B+5");
            Assert.AreEqual(expTree.Evaluate(), 5);

            // Test operator precedence
            expTree = new ExpressionTree("1*5+3");
            Assert.AreEqual(expTree.Evaluate(), 8);
            expTree = new ExpressionTree("1+5*3");
            Assert.AreEqual(expTree.Evaluate(), 16);
            expTree = new ExpressionTree("1+10*2");
            Assert.AreEqual(expTree.Evaluate(), 21);
            expTree = new ExpressionTree("2+15*2");
            Assert.AreEqual(expTree.Evaluate(), 32);

            // Test parenthesis
            expTree = new ExpressionTree("2*(1+3)");
            Assert.AreEqual(expTree.Evaluate(), 8);
            expTree = new ExpressionTree("5/(1+1)");
            Assert.AreEqual(expTree.Evaluate(), 2.5);

            // Test GetVariableNames
            expTree = new ExpressionTree("A1+A2+B3");
            CollectionAssert.AreEqual(expTree.GetVariableNames(), new List<string> { "A1", "A2", "B3" });
        }

        /// <summary>
        /// Contains tests for functionalities of the spreadsheet class.
        /// </summary>
        [TestMethod]
        public void SpreadSheetTests()
        {
            // Test Adding Undos
            SpreadSheet sheet = new SpreadSheet(10, 10);
            sheet.AddUndo(new List<ICommand>(), "Test Commands");
            Assert.AreEqual(1, sheet.UndoRedos.Undos());
            sheet.AddUndo(new List<ICommand>(), "Test Commands");
            Assert.AreEqual(2, sheet.UndoRedos.Undos());

            // Test execute Undo
            sheet.UndoRedos.Undo(sheet);
            Assert.AreEqual(1, sheet.UndoRedos.Undos());
            sheet.UndoRedos.Undo(sheet);
            Assert.AreEqual(0, sheet.UndoRedos.Undos());

            // Test Redo
            Assert.AreEqual(2, sheet.UndoRedos.Redos());

            // Test execution with no Undos
            SpreadSheet sheet2 = new SpreadSheet(10, 10);
            Assert.ThrowsException<InvalidOperationException>(() => sheet2.UndoRedos.Undo(sheet));
            Assert.ThrowsException<InvalidOperationException>(() => sheet2.UndoRedos.Redo(sheet));

            // Test  cell isDefault
            sheet2 = new SpreadSheet(10, 10);
            Assert.IsTrue(sheet2.GetCell(0, 0).IsDefault);
            sheet2.GetCell(1, 1).Text = "test";
            Assert.IsFalse(sheet2.GetCell(1, 1).IsDefault);
            sheet2.GetCell(1, 0).Text = string.Empty;
            Assert.IsTrue(sheet2.GetCell(1, 0).IsDefault);

            // Test Spreadsheet clear
            SpreadSheet sheet3 = new SpreadSheet(10, 10);
            sheet3.GetCell(1, 1).Text = "10";
            sheet3.Clear();
            Assert.AreEqual(sheet3.GetCell(1, 1).Text, string.Empty);
            Assert.AreEqual(sheet3.GetCell(0, 0).Text, string.Empty);

            // Test bad reference
            sheet = new SpreadSheet(10, 10);
            sheet.GetCell(1, 1).Text = "=A1234";
            Assert.IsNull(sheet.GetCell("A1234"));
            Assert.AreEqual("!(bad reference)", sheet.GetCell(1, 1).Value);

            // Test self reference
            sheet.GetCell(0, 0).Text = "=A1";
            Assert.AreEqual("!(self reference)", sheet.GetCell(0, 0).Value);
        }
    }
}
