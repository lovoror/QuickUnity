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
using System.Collections;
using System.Collections.Generic;

namespace QuickUnity.Core.Collections.Generic
{
    /// <summary>
    /// Represents a node in the <see cref="BinaryTree"/>. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the <see cref="BinaryTree{T}"/>.</typeparam>
    /// <seealso cref="System.IComparable"/>
    public sealed class BinaryTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// The element contained in the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        private T m_element;

        /// <summary>
        /// Gets the element contained in the <see cref="BinaryTreeNode{T}"/>.
        /// </summary>
        /// <value>The element contained in the <see cref="BinaryTreeNode{T}"/>.</value>
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
        /// <param name="element">The element to contain in the <see cref="BinaryTree{T}"/>.</param>
        public BinaryTreeNode(T element)
            : this(element, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="element">The element to contain in the <see cref="BinaryTree{T}"/>.</param>
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
        /// The root node of the <see cref="BinaryTree{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_root;

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

        /// <summary>
        /// Remove all nodes from the <see cref="BinaryTree{T}"/>.
        /// </summary>
        public void Clear()
        {
            m_root = null;
        }

        #region Traversing Functions

        /// <summary>
        /// Preorder traverse the <see cref="BinaryTree{T}"/> from the target node.
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
        /// Preorder traverse the <see cref="BinaryTree{T}"/> from the root node.
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
        /// Inorder traverse the <see cref="BinaryTree{T}"/> from the target node.
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
        /// Inorder traverse the <see cref="BinaryTree{T}"/> from the root node.
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
        /// Postorder traverse the <see cref="BinaryTree{T}"/> from the target node.
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
        /// Postorder traverse the <see cref="BinaryTree{T}"/> from the root node.
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

    /// <summary>
    /// Represents binary search tree data structure. A binary search tree is a special kind of
    /// binary tree designed to improve the efficiency of searching through the contents of a binary tree.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the <see cref="BinarySearchTree{T}"/>.</typeparam>
    /// <seealso cref="System.Collections.ICollection"/>
    /// <seealso cref="System.Collections.Generic.ICollection{T}"/>
    /// <seealso cref="System.Collections.IEnumerable"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    public class BinarySearchTree<T> /*: ICollection, ICollection<T>, IEnumerable, IEnumerable<T>*/ where T : IComparable
    {
        /// <summary>
        /// The root node of the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_root;

        /// <summary>
        /// The element comparer instance of the <see cref="BinartSearchTree{T}"/>.
        /// </summary>
        private IComparer<T> m_comparer;

        /// <summary>
        /// The number of elements contained in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        private int m_count;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        public BinarySearchTree()
            : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The element comparer instance of the <see cref="BinartSearchTree{T}"/>.</param>
        public BinarySearchTree(IComparer<T> comparer)
        {
            m_root = null;
            m_comparer = comparer;
        }

        #endregion Constructors

        #region Public Functions

        /// <summary>
        /// Removes all items from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public void Clear()
        {
            m_root = null;
        }

        /// <summary>
        /// Determines whether the <see cref="BinarySerchTree{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="BinarySerchTree{T}"/>.</param>
        /// <returns>
        /// <c>true</c> if item is found in the <see cref="BinarySerchTree{T}"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T item)
        {
            BinaryTreeNode<T> current = m_root;
            int result;

            while (current != null)
            {
                result = m_comparer.Compare(current.element, item);

                if (result == 0)
                {
                    // Found the item.
                    return true;
                }
                else if (result > 0)
                {
                    // current.element > item, so search the left subtree of current.
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    // current.element < item, so search the right subtree of current.
                    current = current.rightChild;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds an item to the <see cerf="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="BinarySearchTree{T}"/>.</param>
        public void Add(T item)
        {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(item);
            BinaryTreeNode<T> current = m_root, parent = null;
            int result;

            while (current != null)
            {
                result = m_comparer.Compare(current.element, item);

                if (result == 0)
                {
                    // equal items, do nothing.
                    return;
                }
                else if (result > 0)
                {
                    // current.element > item, add the node to current's left subtree.
                    parent = current;
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    // current.element < item
                    parent = current;
                    current = current.rightChild;
                }
            }

            m_count++;

            if (parent == null)
            {
                m_root = node;
            }
            else
            {
                result = m_comparer.Compare(parent.element, item);

                if (result > 0)
                {
                    // parent.element > item, therefore node must be added to the left subtree.
                    parent.leftChild = node;
                }
                else
                {
                    // parent.element < item, therefor node must be added to the right substree.
                    parent.rightChild = node;
                }
            }
        }

        /// <summary>
        /// Removes the specific object from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the <see cref="BinarySearchTree{T}"/>;
        /// otherwise, <c>false</c>. This method also returns false if item is not found in the
        /// original <see cref="BinarySearchTree{T}"/>.
        /// </returns>
        public bool Remove(T item)
        {
            if (m_root == null)
            {
                // nothing to remove.
                return false;
            }

            BinaryTreeNode<T> current = m_root, parent = null;
            int result = m_comparer.Compare(current.element, item);

            while (result != 0)
            {
                if (result > 0)
                {
                    // current.element > item, if item exists it's in the left subtree.
                    parent = current;
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    // current.element < item, if item exists it's in the right substree.
                    parent = current;
                    current = current.rightChild;
                }

                if (current == null)
                {
                    return false;
                }
                else
                {
                    result = m_comparer.Compare(current.element, item);
                }
            }

            m_count--;

            // Rebuilds the tree. CASE 1: If current has no right child, then current's left child
            // becomes the node pointed to by the parent.
            if (current.rightChild == null)
            {
                if (parent == null)
                {
                    m_root = current.leftChild;
                }
                else
                {
                    result = m_comparer.Compare(parent.element, current.element);

                    if (result > 0)
                    {
                        parent.leftChild = current.leftChild;
                    }
                    else if (result < 0)
                    {
                        parent.rightChild = current.leftChild;
                    }
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            // replaces current in the tree.
            else if (current.rightChild.leftChild == null)
            {
                current.rightChild.leftChild = current.leftChild;

                if (parent == null)
                {
                    m_root = current.rightChild;
                }
                else
                {
                    result = m_comparer.Compare(parent.element, current.element);

                    if (result > 0)
                    {
                        parent.leftChild = current.rightChild;
                    }
                    else if (result < 0)
                    {
                        parent.rightChild = current.rightChild;
                    }
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            // right child's left-most descendent.
            else
            {
                // Find the left-most child.
                BinaryTreeNode<T> leftmost = current.rightChild.leftChild, leftmostParent = current.rightChild;

                while (leftmost.leftChild != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.leftChild;
                }

                // The parent's left subtree becomes the leftmost's right subtree.
                leftmostParent.leftChild = leftmost.rightChild;

                // Assign leftmost's left and right to current's left and right children.
                leftmost.leftChild = current.leftChild;
                leftmost.rightChild = current.rightChild;

                if (parent == null)
                {
                    m_root = leftmost;
                }
                else
                {
                    result = m_comparer.Compare(parent.element, current.element);

                    if (result > 0)
                    {
                        // parent.Value > current.Value, so make leftmost a left child of parent.
                        parent.leftChild = leftmost;
                    }
                    else if (result < 0)
                    {
                        // parent.Value < current.Value, so make leftmost a right child of parent.
                        parent.rightChild = leftmost;
                    }
                }
            }

            return true;
        }

        #endregion Public Functions
    }
}