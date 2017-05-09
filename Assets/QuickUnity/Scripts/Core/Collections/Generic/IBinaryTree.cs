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
    /// Represents a data structure of binary tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBinaryTree<T> where T : IComparable
    {
        /// <summary>
        /// Gets the root node of the <see cref="IBinaryTree{T}"/>.
        /// </summary>
        /// <value>The root node of the <see cref="IBinaryTree{T}"/>.</value>
        BinaryTreeNode<T> root
        {
            get;
            set;
        }

        /// <summary>
        /// Remove all nodes from the <see cref="IBinaryTree{T}"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Preorder traverse the <see cref="IBinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        void PreorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null);

        /// <summary>
        /// Inorder traverse the <see cref="IBinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        void InorderTraverse(BinaryTreeNode<T> targetNode, Action<T> action = null);

        /// <summary>
        /// Postorder traverse the <see cref="IBinaryTree{T}"/> from the target node.
        /// </summary>
        /// <param name="targetNode">The target node of the binary tree.</param>
        /// <param name="action">The action to handle the element of each node.</param>
        void InorderTraverse(Action<T> action);
    }
}