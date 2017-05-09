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
    /// Specifies traversal methods of the <see cref="BinaryTree{T}"/> class can use.
    /// </summary>
    public enum TraversalMethod
    {
        Preorder,
        Inorder,
        Postorder
    }

    /// <summary>
    /// Represents binary tree data structure.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
    /// <seealso cref="QuickUnity.Core.Collections.Generic.IBinaryTree{T}"/>
    public class BinaryTree<T> : IBinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// The root node of the <see cref="BinaryTree{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_root;

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
        /// <param name="rootElement">The root element to contain in the <see cref="BinaryTree{T}"/>.</param>
        public BinaryTree(T rootElement)
        {
            m_root = new BinaryTreeNode<T>(rootElement);
        }

        #endregion Constructors

        #region Public Functions

        #region IBinaryTree Interface

        /// <summary>
        /// Gets the root node of the <see cref="BinaryTree{T}"/>.
        /// </summary>
        /// <value>The root node of the <see cref="BinaryTree{T}"/>.</value>
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

        /// <summary>
        /// Remove all nodes from the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public void Clear()
        {
            m_root = null;
        }

        /// <summary>
        /// Preorder traverse the <see cref="BinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        public void PreorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            if (action != null)
            {
                action.Invoke(targetNode.value);
            }

            PreorderTraverse(targetNode.leftChild, action);
            PreorderTraverse(targetNode.rightChild, action);
        }

        /// <summary>
        /// Inorder traverse the <see cref="BinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        public void InorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            InorderTraverse(targetNode.leftChild, action);

            if (action != null)
            {
                action.Invoke(targetNode.value);
            }

            InorderTraverse(targetNode.rightChild, action);
        }

        /// <summary>
        /// Postorder traverse the <see cref="BinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        public void PostorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null)
        {
            if (targetNode == null)
            {
                return;
            }

            PostorderTraverse(targetNode.leftChild, action);
            PostorderTraverse(targetNode.rightChild, action);

            if (action != null)
            {
                action.Invoke(targetNode.value);
            }
        }

        #endregion IBinaryTree Interface

        /// <summary>
        /// Preorder traverse the <see cref="BinaryTree{T}"/> from the root node.
        /// </summary>
        /// <param name="action">The action to handle the element of each node.</param>
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
        /// Inorder traverse the <see cref="BinaryTree{T}"/> from the root node.
        /// </summary>
        /// <param name="action">The action to handle the element of each node.</param>
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
        /// Postorder traverse the <see cref="BinaryTree{T}"/> from the root node.
        /// </summary>
        /// <param name="action">The action to handle the element of each node.</param>
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

        #endregion Public Functions
    }
}