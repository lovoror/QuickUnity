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
using System.Collections.Generic;

namespace QuickUnity.Core.Collections.Generic
{
    /// <summary>
    /// Represents a data structure of binary search tree .
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the <see cref="IBinarySearchTree{T}"/>.</typeparam>
    /// <seealso cref="System.Collections.Generic.ICollection{T}"/>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
    public interface IBinarySearchTree<T> : ICollection<T>, IEnumerable<T> where T : IComparable
    {
        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="IBinarySearchTree{T}"/> in a preorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="IBinarySearchTree{T}"/> in a preorder traversal.
        /// </returns>
        IEnumerable<T> preorder
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="IBinarySearchTree{T}"/> in an inorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="IBinarySearchTree{T}"/> in an inorder traversal.
        /// </returns>
        IEnumerable<T> inorder
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be
        /// used to enumerate the items in the <see cref="IBinarySearchTree{T}"/> in a postorder traversal.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/> instance that can be used to
        /// enumerate the items in the <see cref="IBinarySearchTree{T}"/> in a postorder traversal.
        /// </returns>
        IEnumerable<T> postorder
        {
            get;
        }

        /// <summary>
        /// Returns the node that contains the specific item in the <see cref="IBinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IBinarySearchTree{T}"/>.</param>
        /// <returns>The node that contains the specific item. Return null, if not.</returns>
        BinaryTreeNode<T> Find(T item);

        /// <summary>
        /// Copies the nodes of the <see cref="IBinarySearchTree{T}"/> to an <see
        /// cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index, and in
        /// a specified traversal order.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="System.Array"/> that is the destination of the nodes
        /// copied from <see cref="IBinarySearchTree{T}"/>. The <see cref="System.Array"/> must have
        /// zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        /// <param name="traversalMethod">The traversal method for getting each node.</param>
        void CopyTo(T[] array, int arrayIndex, TraversalMethod traversalMethod);

        /// <summary>
        /// Returns an enumerator that iterates through the collection in a specified traversal order.
        /// </summary>
        /// <param name="traversalMethod">The traversal method for getting each node.</param>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<T> GetEnumerator(TraversalMethod traversalMethod);
    }
}