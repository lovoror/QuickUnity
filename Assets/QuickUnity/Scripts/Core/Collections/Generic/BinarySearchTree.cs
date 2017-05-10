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
    /// Represents the data structure of binary search tree . A binary search tree is a special kind
    /// of binary tree designed to improve the efficiency of searching through the contents of a
    /// binary tree.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the <see cref="BinarySearchTree{T}"/>.</typeparam>
    /// <seealso cref="QuickUnity.Core.Collections.Generic.IBinarySearchTree{T}"/>
    /// <seealso cref="System.Collections.Generic.ICollection{T}"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    public class BinarySearchTree<T> : IBinarySearchTree<T>, ICollection<T>, IEnumerable<T> where T : IComparable
    {
        /// <summary>
        /// The root node of the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        private BinaryTreeNode<T> m_root;

        /// <summary>
        /// The element comparer instance of the <see cref="BinartSearchTree{T}"/>.
        /// </summary>
        private readonly IComparer<T> m_comparer;

        /// <summary>
        /// The number of nodes contained in the <see cref="BinarySearchTree{T}"/>.
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
            m_count = 0;
            m_root = null;
            m_comparer = comparer;
        }

        #endregion Constructors

        #region ICollection<T> Interface

        /// <summary>
        /// Gets the number of nodes contained in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <returns>The number of nodes contained in the <see cref="BinarySearchTree{T}"/>.</returns>
        public int Count
        {
            get
            {
                return m_count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="BinarySearchTree{T}"/> is read-only.
        /// </summary>
        /// <returns></returns>
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="BinarySearchTree{T}"/>.</param>
        public void Add(T item)
        {
            // Create a new node for this item.
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(item);
            BinaryTreeNode<T> current = m_root, parent = null;
            int result;

            // Find the right place to insert this node.
            while (current != null)
            {
                result = m_comparer.Compare(current.value, item);

                if (result > 0)
                {
                    parent = current;
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.rightChild;
                }
                else
                {
                    // result == 0 means same items, so quit.
                    return;
                }
            }

            if (parent == null)
            {
                m_root = node;
            }
            else
            {
                result = m_comparer.Compare(parent.value, item);

                if (result > 0)
                {
                    parent.leftChild = node;
                }
                else
                {
                    parent.rightChild = node;
                }
            }

            m_count++;
        }

        /// <summary>
        /// Removes all nodes from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public void Clear()
        {
            m_count = 0;
            m_root = null;
        }

        /// <summary> Determines whether the <see cref="BinarySearchTree{T}"/> contains a specific
        /// value. </summary> <param name="item">The object to locate in the <see
        /// cref="BinarySearchTree{T}"/>.</param> <returns><c>true</c> if item is found in the
        /// ICollection<T>; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        /// <summary>
        /// Copies the nodes of the <see cref="BinarySearchTree{T}"/> to an <see
        /// cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index, and in
        /// a inorder traversal.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the nodes
        /// copied from <see cref="BinarySearchTree{T}"/>. The <see cref="System.Array"/> must have
        /// zero-based indexing.
        /// </param>
        /// <param name="index">The zero-based index in <c>array</c> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex, TraversalMethod.Inorder);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>
        /// <c>true</c> if <c>item</c> was successfully removed from the <see
        /// cref="BinarySearchTree{T}"/>; otherwise, <c>false</c>. This method also returns
        /// <c>false</c> if <c>item</c> is not found in the original <see cref="BinarySearchTree{T}"/>.
        /// </returns>
        public bool Remove(T item)
        {
            if (m_root == null)
            {
                return false;
            }

            BinaryTreeNode<T> current = m_root, parent = null;
            int result;

            // Find the node in the tree.
            while (current != null)
            {
                result = m_comparer.Compare(current.value, item);

                if (result > 0)
                {
                    parent = current;
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.rightChild;
                }
                else
                {
                    break;
                }
            }

            if (current == null)
            {
                return false;
            }

            RemoveNode(current, parent);

            return false;
        }

        #endregion ICollection<T> Interface

        #region I​Enumerable<​T> Interface

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate
        /// through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator(TraversalMethod.Inorder); ;
        }

        #endregion I​Enumerable<​T> Interface

        #region IBinarySearchTree<T> Interface

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="BinarySearchTree{T}"/> in a preorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="BinarySearchTree{T}"/> in a preorder traversal.
        /// </returns>
        public IEnumerable<T> preorder
        {
            get
            {
                // A single stack is sufficient here - it simply maintains the correct order with
                // which to process the children.
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>(Count);
                BinaryTreeNode<T> current = m_root;

                if (m_root != null)
                {
                    stack.Push(m_root);
                }

                while (stack.Count != 0)
                {
                    // Take the top item from the stack.
                    current = stack.Pop();

                    // Add the right and left children, if not null.
                    if (current.rightChild != null) { stack.Push(current.rightChild); }
                    if (current.leftChild != null) { stack.Push(current.leftChild); }

                    // Return the current node.
                    yield return current.value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="BinarySearchTree{T}"/> in an inorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="BinarySearchTree{T}"/> in an inorder traversal.
        /// </returns>
        public IEnumerable<T> inorder
        {
            get
            {
                // A single stack is sufficient - this code was made available by Grant Richins: http://blogs.msdn.com/grantri/archive/2004/04/08/110165.aspx.
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>(Count);

                for (BinaryTreeNode<T> current = m_root; current != null || stack.Count != 0; current = current.rightChild)
                {
                    // Get the left-most item in the subtree, remembering the path taken.
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.leftChild;
                    }

                    current = stack.Pop();
                    yield return current.value;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="BinarySearchTree{T}"/> in a postorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="BinarySearchTree{T}"/> in a postorder traversal.
        /// </returns>
        public IEnumerable<T> postorder
        {
            get
            {
                // Maintain two stacks, one of a list of nodes to visit, and one of booleans,
                // indicating if the node has been processed or not.
                Stack<BinaryTreeNode<T>> toVisit = new Stack<BinaryTreeNode<T>>(Count);
                Stack<bool> hasBeenProcessed = new Stack<bool>(Count);
                BinaryTreeNode<T> current = m_root;

                if (current != null)
                {
                    toVisit.Push(current);
                    hasBeenProcessed.Push(false);
                    current = current.leftChild;
                }

                while (toVisit.Count != 0)
                {
                    if (current != null)
                    {
                        // Add this node to the stack with a false processed value.
                        toVisit.Push(current);
                        hasBeenProcessed.Push(false);
                        current = current.leftChild;
                    }
                    else
                    {
                        // See if the node on the stack has been processed.
                        bool processed = hasBeenProcessed.Pop();
                        BinaryTreeNode<T> node = toVisit.Pop();

                        if (!processed)
                        {
                            // If it's not been processed, "recurse" down the right subtree.
                            toVisit.Push(node);
                            hasBeenProcessed.Push(true);    // It's now been processed.
                            current = node.rightChild;
                        }
                        else
                        {
                            yield return node.value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the node that contains the specific item in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="BinarySearchTree{T}"/>.</param>
        /// <returns>The node that contains the specific item. Return null, if not.</returns>
        public BinaryTreeNode<T> Find(T item)
        {
            BinaryTreeNode<T> current = m_root;
            int result;

            while (current != null)
            {
                result = m_comparer.Compare(current.value, item);

                if (result > 0)
                {
                    current = current.leftChild;
                }
                else if (result < 0)
                {
                    current = current.rightChild;
                }
                else
                {
                    return current;
                }
            }

            return null;
        }

        /// <summary>
        /// Copies the nodes of the <see cref="BinarySearchTree{T}"/> to an <see
        /// cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index, and in
        /// a specified traversal order.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the nodes
        /// copied from <see cref="BinarySearchTree{T}"/>. The <see cref="System.Array"/> must have
        /// zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <param name="traversalMethod">The traversal method for getting each node.</param>
        public void CopyTo(T[] array, int arrayIndex, TraversalMethod traversalMethod)
        {
            IEnumerable<T> enumerable = null;

            if (traversalMethod == TraversalMethod.Preorder)
            {
                enumerable = preorder;
            }
            else if (traversalMethod == TraversalMethod.Inorder)
            {
                enumerable = inorder;
            }
            else if (traversalMethod == TraversalMethod.Postorder)
            {
                enumerable = postorder;
            }

            // dump the contents of the tree into the passed-in array
            int i = 0;

            foreach (T value in enumerable)
            {
                array[i + arrayIndex] = value;
                i++;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection in a specified traversal order.
        /// </summary>
        /// <param name="traversalMethod">The traversal method for getting each node.</param>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator(TraversalMethod traversalMethod)
        {
            if (traversalMethod == TraversalMethod.Preorder)
            {
                return preorder.GetEnumerator();
            }
            else if (traversalMethod == TraversalMethod.Postorder)
            {
                return postorder.GetEnumerator();
            }
            else
            {
                return inorder.GetEnumerator();
            }
        }

        #endregion IBinarySearchTree<T> Interface

        #region Private Functions

        /// <summary>
        /// Removes the specific <see cref="BinaryTreeNode{T}"/> from the <see
        /// cref="BinarySearchTree{T}"/>, and rebuild the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="node">The target node to be removed from the <see cref="BinarySearchTree{T}"/>.</param>
        /// <param name="parent">The parent of node in the <see cref="BinarySearchTree{T}"/>.</param>
        private void RemoveNode(BinaryTreeNode<T> node, BinaryTreeNode<T> parent)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            BinaryTreeNode<T> target = null;
            m_count--;

            if (node.rightChild == null)
            {
                // Case 1: current has only smaller children, therefore the left side replaces current.
                target = node.leftChild;
            }
            else if (node.rightChild.leftChild == null)
            {
                // Case 2: right side has only bigger children, therefore the right side replaces current.
                target = node.rightChild;
                target.leftChild = node.leftChild;
            }
            else
            {
                // Case 3: right side has smaller children, find the smallest and replace current.
                BinaryTreeNode<T> leftParent = node.rightChild;
                target = node.rightChild.leftChild;

                while (target.leftChild != null)
                {
                    leftParent = target;
                    target = target.leftChild;
                }

                // The parent's left subtree becomes the targets right subtree.
                leftParent.leftChild = target.rightChild;

                // Assign target's left and right to current's left and right children.
                target.leftChild = node.leftChild;
                target.rightChild = node.rightChild;
            }

            // Attach the remaining nodes.
            if (parent == null)
            {
                m_root = target;
            }
            else
            {
                int result = m_comparer.Compare(parent.value, node.value);

                if (result > 0)
                {
                    parent.leftChild = target;
                }
                else if (result < 0)
                {
                    parent.rightChild = target;
                }
            }
        }

        #endregion Private Functions
    }
}