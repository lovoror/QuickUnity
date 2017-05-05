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
using System.Collections.ObjectModel;

namespace QuickUnity.Core.Collections.Generic
{
    /// <summary>
    /// Represents a list of <see cref="TreeNode"/>.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the tree.</typeparam>
    /// <seealso cref="System.Collections.ObjectModel.Collection{QuickUnity.Core.Collections.Generic.TreeNode{T}}"/>
    public class TreeNodeList<T> : Collection<TreeNode<T>> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeList{T}"/> class.
        /// </summary>
        public TreeNodeList()
            : base()
        {
        }
    }

    /// <summary>
    /// Represents a node in the Tree&lt;T&gt;. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the tree.</typeparam>
    public sealed class TreeNode<T> where T : IComparable
    {
    }

    /// <summary>
    /// Represents tree data structure.
    /// </summary>
    /// <typeparam name="T">Specifies the element type of the tree.</typeparam>
    public class Tree<T> where T : IComparable
    {
    }
}