/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System;

namespace QuickUnity.Core.Collections.Generic
{
    /// <summary>
    /// Represents a node in the <see cref="BinaryTree"/>. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the <see cref="BinaryTree{T}"/>.</typeparam>
    public sealed class BinaryTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// The value contained in the node.
        /// </summary>
        private T m_value;

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        /// <value>The value contained in the node.</value>
        public T value
        {
            get
            {
                return m_value;
            }

            set
            {
                m_value = value;
            }
        }

        /// <summary>
        /// The left child node of the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_leftChild;

        /// <summary>
        /// Gets the left child node of the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        /// <value>The left child node of the <see cref="BinaryTreeNode{T}"/>.</value>
        public BinaryTreeNode<T> leftChild
        {
            get
            {
                return m_leftChild;
            }

            set
            {
                m_leftChild = value;
            }
        }

        /// <summary>
        /// The right child node of the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_rightChild;

        /// <summary>
        /// Gets the right child node of the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        /// <value>The right child node of the <see cref="BinaryTreeNode{T}"/>.</value>
        public BinaryTreeNode<T> rightChild
        {
            get
            {
                return m_rightChild;
            }

            set
            {
                m_rightChild = value;
            }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value to contained in the node.</param>
        public BinaryTreeNode(T value)
            : this(value, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value to contained in the node.</param>
        /// <param name="leftChild">The left child node of this node.</param>
        /// <param name="rightChild">The right child node of this node.</param>
        public BinaryTreeNode(T value, BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild)
        {
            m_value = value;
            m_leftChild = leftChild;
            m_rightChild = rightChild;
        }

        #endregion Constructors
    }
}