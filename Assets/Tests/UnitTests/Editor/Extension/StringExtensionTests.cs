using NUnit.Framework;
using UnityEngine;

namespace QuickUnity.Extensions
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Extensions.StringExtension"/>.
	/// </summary>
	[TestFixture]
	internal class StringExtensionTests
	{	
		/// <summary>
		/// Test case for TrimAll extension method for <see cref="System.String"/>.
		/// </summary>
		[Test]
		public void TrimAllTest([Values(null, new char[] { 'a' })] char[] chars)
		{
			string source = "Test trim method a a a a";
			string expected = null;

			if(chars == null)
			{
				expected = "Testtrimmethodaaaa";
			}
			else
			{
				expected = "Test trim method    ";
			}

			string actual = source.TrimAll(chars);
			Assert.AreEqual(expected, actual, "TrimAll method got wrong string!");
		}

		/// <summary>
		/// Test case for ToVector2 extension method for <see cref="System.String"/>.
		/// </summary>
		[Test]
		public void ToVector2Test()
		{
			string source = "(1, 2)";
			Vector2 expected = new Vector2(1, 2);
			Vector2 actual = source.ToVector2();
			Assert.AreEqual(expected, actual, "ToVector2 method got wrong Vector2 object");
		}

		/// <summary>
		/// Test case for ToVector3 extension method for <see cref="System.String"/>.
		/// </summary>
		[Test]
		public void ToVector3Test()
		{
			string source = "(1, 2, 3)";
			Vector3 expected = new Vector3(1, 2, 3);
			Vector3 actual = source.ToVector3();
			Assert.AreEqual(expected, actual, "ToVector3 method got wrong Vector3 object");
		}

		/// <summary>
		/// Test case for ToQuaternion extension method for <see cref="System.String"/>.
		/// </summary>
		[Test]
		public void ToQuaternionTest()
		{
			string source = "(1, 2, 3, 4)";
			Quaternion expected = new Quaternion(1, 2, 3, 4);
			Quaternion actual = source.ToQuaternion();
			Assert.AreEqual(expected, actual, "ToQuaternion method got wrong Quaternion object");
		}
	}
}