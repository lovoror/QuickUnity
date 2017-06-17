using NUnit.Framework;
using System.Collections;

namespace QuickUnity.Extensions.Collections
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions.ArrayListExtension"/>.
	/// </summary>
	[TestFixture]
	internal class ArrayListExtensionTests
	{	
		/// <summary>
		/// Test case for AddUnique extension method for <see cref="System.Collections.ArrayList"/>.
		/// </summary>
		[Test]
		public void AddUniqueTest()
		{
			ArrayList list = new ArrayList() { 1, "2", 3.012 };
			ArrayList expected = new ArrayList() { 1, "2", 3.012 };
			list.AddUnique(1);
			list.AddUnique("2");
			list.AddUnique(3.012);
			CollectionAssert.AreEqual(expected, list, "AddUnique method got no expected result");
		}

		/// <summary>
		/// Test case for AddRangeUnique extension method for <see cref="System.Collections.ArrayList"/>.
		/// </summary>
		[Test]
		public void AddRangeUniqueTest()
		{
			ArrayList list = new ArrayList() { 1, "2", 3.012 };
			ArrayList expected = new ArrayList() { 1, "2", 3.012, 4 };
			list.AddRangeUnique(new ArrayList() { "2", 1, 4 });
			CollectionAssert.AreEqual(expected, list, "AddRangeUnique method got no expected result");
		}
	}
}