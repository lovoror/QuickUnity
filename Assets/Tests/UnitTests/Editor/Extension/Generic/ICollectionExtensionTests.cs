using NUnit.Framework;
using QuickUnity.Extensions.Generic;
using System.Collections.Generic;

namespace QuickUnity.UnitTests.Extensions.Generic
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions.Generic.ICollectionExtension"/>.
	/// </summary>
	[TestFixture]
	internal class ICollectionExtensionTests
	{
		/// <summary>
		/// Test case for AddUnique extension method for <see cref="System.Collections.Generic.ICollection{T}"/>.
		/// </summary>
		[Test]
		public void AddUniqueTest()
		{
			List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
			list.AddUnique(5);
			int[] expected = new int[] { 1, 2, 3, 4, 5 };
			int[] actual = list.ToArray();
			CollectionAssert.AreEqual(expected, actual, "Method AddUnique didn't work correctly!");
		}
	}
}
