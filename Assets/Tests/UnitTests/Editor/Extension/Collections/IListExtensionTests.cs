using NUnit.Framework;
using QuickUnity.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Extensions.Collections
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions.IListExtension"/>.
	/// </summary>
	[TestFixture]
	internal class IListExtensionTests
	{
		/// <summary>
		/// Test case for AddUnique extension method for <see cref="System.Collections.Generic.ICollection{T}"/>.
		/// </summary>
		[Test]
		public void AddUniqueTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			(list as IList).AddUnique(5);
			int[] expected = new int[] { 1, 2, 3, 4, 5 };
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Method AddUnique didn't work correctly!");
		}

		/// <summary>
		/// Test case for Swap extension method for <see cref="System.Collections.IList"/>.
		/// </summary>
		[Test]
		public void SwapTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			int[] expected = new int[] { 4, 2, 3, 1, 5 };
			(list as IList).Swap(0, 3);
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Swap elements is not correct");
		}
		
		/// <summary>
		/// Test case for CopyTo extension method for <see cref="System.Collections.IList"/>.
		/// </summary>
		[Test]
		public void CopyToTest()
		{
			IList list = new int[5] { 1, 2, 3, 4, 5 };
			int[] expected = new int[] { 2, 3, 4, 5};
			IList actual = new int[4];
			list.CopyTo(actual, 1);
			CollectionAssert.AreEqual(expected, actual, "Method CopyTo didn't work correctly!");
		}

		/// <summary>
		/// Test case for IndexOfMax extension method for <see cref="System.Collections.IList"/>.
		/// </summary>
		[Test]
		public void IndexOfMaxTest()
		{
			IList list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int expected = 5;
			int actual = list.IndexOfMax(list.Count);
			Assert.AreEqual(expected, actual, "Method IndexOfMax didn't work correctly!");
		}

		/// <summary>
		/// Test case for ToArrayString extension method for <see cref="System.Collections.IList"/>.
		/// </summary>
		[Test]
		public void ToArrayStringTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			string actual = (list as IList).ToArrayString();
			string expected = "{ 1, 2, 3, 4, 5 }";
			Assert.AreEqual(expected, actual, string.Format("The array string is not corrected: {0}", actual));
		}
	}
}