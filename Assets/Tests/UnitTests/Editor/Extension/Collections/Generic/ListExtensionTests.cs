using NUnit.Framework;
using QuickUnity.Extensions.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Extensions.Collections.Generic
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions.ListExtension"/>.
	/// </summary>
	[TestFixture]
	internal class ListExtensionTests
	{
		/// <summary>
		/// Test case for AddRangeUnique extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void AddRangeUniqueTest()
		{
			List<int> list = new List<int>() { 1, 2, 3 };
			list.AddRangeUnique(new int[] { 2, 3, 4, 5 });
			int[] expected = new int[] { 1, 2, 3, 4, 5 };
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Method List<T>.AddRangeUnique didn't work correctly!");
		}

		/// <summary>
		/// Test case for RemoveRange extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void RemoveRangeTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			list.RemoveRange(new int[] { 4, 5 });
			int[] expected = new int[] { 1, 2, 3 };
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Method List<T>.RemoveRange didn't work correctly!");
		}

		/// <summary>
		/// Test case for Swap extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void SwapTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			int[] expected = new int[] { 4, 2, 3, 1, 5 };
			list.Swap(0, 3);
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Swap elements is not correct");
		}

		/// <summary>
		/// Test case for ToArrayString extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void ToArrayStringTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			string actual = list.ToArrayString();
			string expected = "{ 1, 2, 3, 4, 5 }";
			Assert.AreEqual(expected, actual, string.Format("The array string is not corrected: {0}", actual));
		}

		/// <summary>
		/// Test case for BubbleSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void BubbleSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.BubbleSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Bubble sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for CocktailSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void CocktailSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.CocktailSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Cocktail sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for SelectionSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void SelectionSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.SelectionSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Selection sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for InsertionSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void InsertionSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.InsertionSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Insertion sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for BinaryInsertionSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void BinaryInsertionSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.BinaryInsertionSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Binary insertion sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for ShellSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void ShellSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.ShellSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Shell sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for MergeSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void MergeSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.MergeSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Merge sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for HeapSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void HeapSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.HeapSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Heap sorting did not sort correctly");
		}

		/// <summary>
		/// Test case for QuickSort extension method for <see cref="System.Collections.Generic.List{T}"/>.
		/// </summary>
		[Test]
		public void QuickSortTest()
		{
			List<int> list = new List<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.QuickSort();
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Quick sorting did not sort correctly");
		}
	}
}