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
    /// Represents a node in the BinaryTree&lt;T&gt;. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    public sealed class BinaryTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// The element contained in the node.
        /// </summary>
        private T m_element;

        /// <summary>
        /// Gets the element contained in the node.
        /// </summary>
        /// <value>The element contained in the node.</value>
        public T element
        {
            get
            {
                return m_element;
            }

            set
            {
                m_element = value;
            }
        }

        /// <summary>
        /// The left child node of this node.
        /// </summary>
        private BinaryTreeNode<T> m_leftChild;

        /// <summary>
        /// Gets the left child node of this node.
        /// </summary>
        /// <value>The left child node of this node.</value>
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
        /// The right child node of this node.
        /// </summary>
        private BinaryTreeNode<T> m_rightChild;

        /// <summary>
        /// Gets the right child node of this node.
        /// </summary>
        /// <value>The right child node of this node.</value>
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
        /// <param name="element">The element to contain in the BinaryTree&lt;T&gt;.</param>
        public BinaryTreeNode(T element)
            : this(element, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="element">The element to contain in the BinaryTree&lt;T&gt;.</param>
        /// <param name="leftChild">The left child node of this node.</param>
        /// <param name="rightChild">The right child node of this node.</param>
        public BinaryTreeNode(T element, BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild)
        {
            m_element = element;
            m_leftChild = leftChild;
            m_rightChild = rightChild;
        }

        #endregion Constructors
    }

    /// <summary>
    /// Represents binary tree data structure.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    public class BinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// The root node of this binary tree.
        /// </summary>
        protected BinaryTreeNode<T> m_root;

        /// <summary>
        /// Gets the root node of this binary tree.
        /// </summary>
        /// <value>The root node of this binary tree.</value>
        public BinaryTreeNode<T> root
        {
            get
            {
                return m_root;
            }

            set
            {
                m_root = value;
            }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTree{T}"/> class.
        /// </summary>
        public BinaryTree()
        {
            m_root = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTree{T}"/> class.
        /// </summary>
        /// <param name="rootElement">The root element to contain in the BinaryTree&lt;T&gt;.</param>
        public BinaryTree(T rootElement)
        {
            m_root = new BinaryTreeNode<T>(rootElement);
        }

        #endregion Constructors

        #region Public Functions

        #region Traversing Functions

        /// <summary>
        /// Preorder traverse the binary tree.
        /// </summary>
        /// <param name="targetNode">The target binary tree node.</param>
        /// <param name="action">The action to handle the element of the node.</param>
        public void PreorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            if (action != null)
            {
                action.Invoke(targetNode.element);
            }

            PreorderTraverse(targetNode.leftChild);
            PreorderTraverse(targetNode.rightChild);
        }

        /// <summary>
        /// Preorder traverse the binary tree.
        /// </summary>
        /// <param name="action">The action to handle the element of the node.</param>
        public void PreorderTraverse(Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            if (m_root != null)
            {
                PreorderTraverse(m_root, action);
            }
        }

        /// <summary>
        /// Inorder traverse the binary tree.
        /// </summary>
        /// <param name="targetNode">The target binary tree node.</param>
        /// <param name="action">The action to handle the element of the node.</param>
        public void InorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            InorderTraverse(targetNode.leftChild);

            if (action != null)
            {
                action.Invoke(targetNode.element);
            }

            InorderTraverse(targetNode.rightChild);
        }

        /// <summary>
        /// Inorder traverse the binary tree.
        /// </summary>
        /// <param name="action">The action to handle the element of the node.</param>
        public void InorderTraverse(Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            if (m_root != null)
            {
                InorderTraverse(m_root, action);
            }
        }

        /// <summary>
        /// Postorder traverse the binary tree.
        /// </summary>
        /// <param name="targetNode">The target binary tree node.</param>
        /// <param name="action">The action to handle the element of the node.</param>
        public void PostorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            PostorderTraverse(targetNode.leftChild);
            PostorderTraverse(targetNode.rightChild);

            if (action != null)
            {
                action.Invoke(targetNode.element);
            }
        }

        /// <summary>
        /// Postorder traverse the binary tree.
        /// </summary>
        /// <param name="action">The action to handle the element of the node.</param>
        public void PostorderTraverse(Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            if (m_root != null)
            {
                PostorderTraverse(m_root, action);
            }
        }

        #endregion Traversing Functions

        #endregion Public Functions
    }
}