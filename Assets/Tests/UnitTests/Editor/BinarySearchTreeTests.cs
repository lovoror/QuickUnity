using NUnit.Framework;
using QuickUnity.Core.Collections.Generic;
using System.Collections.Generic;

namespace QuickUnity.UnitTests
{
	/// <summary>
	/// Unit test cases for class <see cref="QuickUnity.Core.Collections.Generic.BinarySearchTree{T}"/>.
	/// </summary>
	[TestFixture]
    [Category("BinarySearchTreeTests")]
	internal class BinarySearchTreeTests
	{
		/// <summary>
		/// Test case for property Count.
		/// </summary>
		[Test]
		public void CountTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			Assert.AreEqual(12, bst.Count, string.Format("Property Count return wrong number: {0}, expected number is: {1}", bst.Count, 12));
		}

		/// <summary>
		/// Test case for method Add.
		/// </summary>
		[Test]
		public void AddTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			bst.Add(55);
            bst.Add(-55);
            bst.Add(35);

			Assert.IsTrue(bst.Contains(55), "Did not add element correctly");
            Assert.IsTrue(bst.Contains(-55), "Did not add element correctly");
            Assert.IsTrue(bst.Contains(35), "Did not add element correctly");
		}

		/// <summary>
		/// Test case for method Clear.
		/// </summary>
		[Test]
		public void ClearTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			bst.Clear();
			Assert.AreEqual(0, bst.Count, "Clearing the BinarySearchTree did not work");
		}

		/// <summary>
		/// Test case for method Contains.
		/// </summary>
		[Test]
		public void ContainsTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			Assert.IsTrue(bst.Contains(0), "Contains method could not find element");
		}

		/// <summary>
		/// Test case for method CopyTo.
		/// </summary>
		[Test]
        public void CopyToTest()
        {
            IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 5, 20, 25, 50, 66, 75, 80, 90, 92, 95, 111, 150, 166, 175, 200 };
            int[] actual = new int[bst.Count];

            // Default is InOrder
            bst.CopyTo(actual, 0);

            CollectionAssert.AreEqual(expected, actual, "Inorder traversal did not sort correctly");
        }

		/// <summary>
		/// Test case for method CopyTo with parameter TraversalMethod.Preorder.
		/// </summary>
		[Test]
        public void CopyToPreOrderTest()
        {
            IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 90, 50, 20, 5, 25, 75, 66, 80, 150, 95, 92, 111, 175, 166, 200 };
            int[] actual = new int[bst.Count];
            bst.CopyTo(actual, 0, TraversalMethod.Preorder);

            CollectionAssert.AreEqual(expected, actual, "Preorder traversal did not sort correctly");
        }

		/// <summary>
		/// Test case for method CopyTo with parameter TraversalMethod.Postorder.
		/// </summary>
		[Test]
		public void CopyToPostOrderTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 5, 25, 20, 66, 80, 75, 50, 92, 111, 95, 166, 200, 175, 150, 90 };
            int[] actual = new int[bst.Count];
            bst.CopyTo(actual, 0, TraversalMethod.Postorder);

            CollectionAssert.AreEqual(expected, actual, "Postorder traversal did not sort correctly");
		}

		/// <summary>
		/// Test case for method Remove.
		/// </summary>
		[Test]
		public void RemoveTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
            bst.Remove(20);
            bst.Remove(0);
            bst.Remove(10);

            Assert.IsFalse(bst.Contains(20), "Did not remove element correctly");
            Assert.IsFalse(bst.Contains(0), "Did not remove element correctly");
            Assert.IsFalse(bst.Contains(10), "Did not remove root element correctly");
		}

		/// <summary>
		/// Test case for method GetEnumerator.
		/// </summary>
		[Test]
		public void GetEnumeratorTest()
        {
            IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 5, 20, 25, 50, 66, 75, 80, 90, 92, 95, 111, 150, 166, 175, 200 };
            List<int> actual = new List<int>();

            // Iterate trough the collection, the default enumarator is InOrder.
            foreach (int value in bst) 
			{ 
				actual.Add(value); 
			}

            CollectionAssert.AreEqual(expected, actual, "Inorder traversal did not work correctly");
        }

		/// <summary>
		/// Test case for method GetEnumerator with parameter TraversalMethod.Preorder.
		/// </summary>
		[Test]
		public void GetEnumeratorPreOrderTest()
        {
            IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 90, 50, 20, 5, 25, 75, 66, 80, 150, 95, 92, 111, 175, 166, 200 };
            List<int> actual = new List<int>();

            // Iterate trough the collection, the default enumarator is InOrder.
            foreach (int value in bst.preorder)
			{ 
				actual.Add(value);
			}

            CollectionAssert.AreEqual(expected, actual, "Preorder traversal did not work correctly");
        }

		/// <summary>
		/// Test case for method GetEnumerator with parameter TraversalMethod.Postorder.
		/// </summary>
		[Test]
		public void GetEnumeratorPostOrderTest()
		{
			IBinarySearchTree<int> bst = new BinarySearchTree<int>() { 90, 50, 150, 20, 75, 95, 175, 5, 25, 66, 80, 92, 111, 166, 200 };
            int[] expected = new int[] { 5, 25, 20, 66, 80, 75, 50, 92, 111, 95, 166, 200, 175, 150, 90 };
            List<int> actual = new List<int>();

            // Iterate trough the collection, the default enumarator is PostOrder.
            foreach (int value in bst.postorder)
			{ 
				actual.Add(value);
			}

            CollectionAssert.AreEqual(expected, actual, "Postorder traversal did not work correctly");
		}
	}
}