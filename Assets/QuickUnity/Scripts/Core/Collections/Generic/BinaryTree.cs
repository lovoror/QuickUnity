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
	/// The node of Binary Tree.
	/// <typeparam name="T">Specifies the element type of the binary tree.</typeparam>
	/// </summary>
	public sealed class BinaryTreeNode<T> where T : IComparable<T>
	{
		/// <summary>
		/// The <see cref="BinaryTree"/> that the BinaryTreeNode<T> belongs to.
		/// </summary>
		private BinaryTree<T> m_tree;
		
		/// <summary>
		/// Gets the <see cref="BinaryTree"/> that the BinaryTreeNode<T> belongs to.
		/// </summary>
		/// <returns>The <see cref="BinaryTree"/> that the BinaryTreeNode<T> belongs to.</returns>
		public BinaryTree<T> tree
		{
			get
			{
				return m_tree;
			}
		}

		private BinaryTreeNode<T> m_leftChild;

		public BinaryTreeNode<T> leftChild
		{
			get
			{
				return m_leftChild;
			}
		}

		private BinaryTreeNode<T> m_rightChild;

		public BinaryTreeNode<T> rightChild
		{
			get
			{
				return m_rightChild;
			}
		}

		/// <summary>
		/// The value contained in the node.
		/// </summary>
		private T m_value;

		/// <summary>
		/// Gets the value contained in the node.
		/// </summary>
		/// <returns>The value contained in the node.</returns>
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
		/// Initializes a new instance of the <see cref="BinaryTreeNode"/> class.
		/// </summary>
		/// <param name="value">The value contained in the node.</param>
		public BinaryTreeNode(T value)
		{
			m_value = value;
			m_leftChild = null;
			m_rightChild = null;
		}
	}

	/// <summary>
	/// Binary Tree Data Structure.
	/// </summary>
    public class BinaryTree<T> where T : IComparable<T>
	{
		/// <summary>
		/// The root node of the binray tree.
		/// </summary>
		private BinaryTreeNode<T> m_root;

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryTree"/> class.
		/// </summary>
		public BinaryTree()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryTree"/> class.
		/// </summary>
		/// <param name="rootValue">The value contained in the root node.</param>
		public BinaryTree(T rootValue)
		{
			m_root = new BinaryTreeNode<T>(rootValue);
		}

		public void Insert(T value)
		{
			Insert(value, m_root);
		}

		public void Insert(T value, BinaryTreeNode<T> node)
		{
			
		}
    }
}