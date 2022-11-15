// <copyright file="OperatorNodeFactory.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    /// <summary>
    /// Class for creating operator nodes.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Dictionary to store current operator types.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Delegate for creating functions with operators.
        /// </summary>
        /// <param name="op"> the operator char. </param>
        /// <param name="type"> the property type. </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Creates an operator node based on the passed char.
        /// </summary>
        /// <param name="op"> the char operator. </param>
        /// <returns> an OperatorNode. </returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            // If an OperatorNode cannot be created, simply return null
            return null;
        }

        /// <summary>
        /// Applies a function with each value in the operators dictionary.
        /// </summary>
        /// <param name="onOperator"> the OnOperator function. </param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property.
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object value = operatorField.GetValue(type);

                        // If the property is not static, use the following code instead:
                        // object value = operatorField.GetValue(Activator.CreateInstance(type, new ConstantNode("0"), new ConstantNode("0")));
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
