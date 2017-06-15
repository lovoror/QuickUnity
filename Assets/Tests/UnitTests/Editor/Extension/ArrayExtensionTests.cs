using NUnit.Framework;
using QuickUnity.Extensions.Collections;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Unit test cases for class <see cref="QuickUnity.Extensions.ArrayExtension"/>.
    /// </summary>
    [TestFixture]
	internal class ArrayExtensionTests
    {
        /// <summary>
        /// Test case for Swap extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void SwapTest()
        {
            int[] arr = new int[5] { 1, 2, 3, 4, 5 };
			int[] expected = new int[] { 4, 2, 3, 1, 5 };
			arr.Swap(0, 3);
			CollectionAssert.AreEqual(expected, arr, "Swap elements is not correct");
        }

        /// <summary>
		/// Test case for BubbleSort extension method for <see cref="System.Array"/>.
		/// </summary>
		[Test]
		public void BubbleSortTest()
		{
			int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.BubbleSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Bubble sorting did not sort correctly");
		}

        /// <summary>
        /// Test case for CocktailSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void CocktailSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.CocktailSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Cocktail sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for SelectionSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void SelectionSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.SelectionSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Selection sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for InsertionSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void InsertionSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.InsertionSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Insertion sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for BinaryInsertionSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void BinaryInsertionSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.BinaryInsertionSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Binary insertion sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for ShellSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void ShellSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.ShellSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Shell sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for MergeSort extension method for <see cref="System.Array"/>.
        /// </summary>
        [Test]
        public void MergeSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.MergeSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Merge sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for HeapSort extension method for <see cref="System.Array"/>. 
        /// </summary>
        [Test]
        public void HeapSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.HeapSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Heap sorting did not sort correctly");
        }

        /// <summary>
        /// Test case for QuickSort extension method for <see cref="System.Array"/>. 
        /// </summary>
        [Test]
        public void QuickSortTest()
        {
            int[] list = new int[] { 10, 15, 5, 4, 7, 20, 14, 0, -5, -8, -2, -1 };
			int[] expected = new int[] { -8, -5, -2, -1, 0, 4, 5, 7, 10, 14, 15, 20 };
			list.QuickSort();
			int[] actual = list;
			CollectionAssert.AreEqual(expected, actual, "Quick sorting did not sort correctly");
        }
    }
}