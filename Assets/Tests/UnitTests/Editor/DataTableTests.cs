using NUnit.Framework;
using QuickUnity.Data;
using Tests.UnitTests.Editor.DataTables;

namespace QuickUnity.Tests.UnitTests
{
    /// <summary>
    /// Unit test cases for class DataTableManager.
    /// </summary>
    [TestFixture]
    [Category("QuickUnity Tests/Unit Tests/DataTable Test")]
    internal class DataTableTests
    {
        /// <summary>
        /// Test for the method DataTableManager.GetDataTableRow.
        /// </summary>
        [Test]
        public void GetDataTableRowTest()
        {
            TestData testData = DataTableManager.instance.GetDataTableRow<TestData>(1L);
            DataTableManager.instance.Dispose();

            if (testData != null)
            {
                Assert.AreEqual(testData.testInt, 2147483647);
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test for the method DataTableManager.GetAllDataTableRow.
        /// </summary>
        [Test]
        public void GetAllDataTableRowTest()
        {
            TestDataTwo[] array = DataTableManager.instance.GetAllDataTableRow<TestDataTwo>();
            DataTableManager.instance.Dispose();
            Assert.IsNotNull(array);
        }
    }
}