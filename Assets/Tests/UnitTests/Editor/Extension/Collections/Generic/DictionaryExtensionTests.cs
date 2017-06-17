using NUnit.Framework;
using System.Collections.Generic;

namespace QuickUnity.Extensions.Collections.Generic
{	
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions."/>.
	/// </summary>
	[TestFixture]
	internal class DictionaryExtensionTests
	{	
		/// <summary>
		/// Test case for AddUnique extension method for <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
		/// </summary>
		[Test]
		public void AddUniqueTest()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>() 
			{
				{ "test1", 1 },
				{ "test2", 2 },
				{ "test3", 3 }
			};

			Dictionary<string, int> expected = new Dictionary<string, int>()
			{
				{ "test1", 1 },
				{ "test2", 2 },
				{ "test3", 3 },
				{ "test4", 4 }
			};

			dict.AddUnique("test3", 3);
			dict.AddUnique("test4", 4);
			CollectionAssert.AreEqual(expected, dict, "AddUnique got no expected results");
		}

		/// <summary>
		/// Test case for GetKey extension method for <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/>.
		/// </summary>
		[Test]
		public void GetKeyTest()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>() 
			{
				{ "test1", 1 },
				{ "test2", 2 },
				{ "test3", 3 }
			};

			string expected = "test2";
			string actual = dict.GetKey(2);
			Assert.AreEqual(expected, actual, "GetKey got no expected result");
		}
	}
}