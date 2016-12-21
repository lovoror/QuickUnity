using NUnit.Framework;
using QuickUnity.Data;
using System.Collections.Generic;
using Tests.UnitTests.Editor.DataTables;

namespace QuickUnity.UnitTests
{
    /// <summary>
    /// Unit test cases for class DataTableManager.
    /// </summary>
    [TestFixture]
    [Category("DataTableTest")]
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
        /// Test for the method DataTableManager.GetDataTableRows.
        /// </summary>
        [Test]
        public void GetDataTableRowsTest()
        {
            List<BoxDbQueryCondition> conditions = new List<BoxDbQueryCondition>()
            {
                new BoxDbQueryCondition("testUShort", (ushort)0),
                new BoxDbQueryCondition("testBoolean", false)
            };

            List<BoxDbMultiConditionOperator> multiConditionOps = new List<BoxDbMultiConditionOperator>()
            {
                BoxDbMultiConditionOperator.Or
            };

            TestData[] results = DataTableManager.instance.GetDataTableRows<TestData>(conditions, multiConditionOps);
            DataTableManager.instance.Dispose();

            if (results != null)
            {
                Assert.Pass();
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
            TestDataTwo[] array = DataTableManager.instance.GetAllDataTableRows<TestDataTwo>();
            DataTableManager.instance.Dispose();
            Assert.IsNotNull(array);
        }

        /// <summary>
        /// Test for the method DataTableManager.GetAllDataTableRowsCount.
        /// </summary>
        [Test]
        public void GetAllDataTableRowsCountTest()
        {
            long count = DataTableManager.instance.GetAllDataTableRowsCount<TestData>();
            DataTableManager.instance.Dispose();
            Assert.AreEqual(3L, count);
        }

        /// <summary>
        /// Test for the method DataTableManager.GetDataTableRowsCount.
        /// </summary>
        [Test]
        public void GetDataTableRowsCountTest()
        {
            List<BoxDbQueryCondition> conditions = new List<BoxDbQueryCondition>()
            {
                new BoxDbQueryCondition("testInt", 2147483647),
                new BoxDbQueryCondition("testBoolean", true),
                new BoxDbQueryCondition("testUInt", (uint)0)
            };

            List<BoxDbMultiConditionOperator> multiConditionOps = new List<BoxDbMultiConditionOperator>()
            {
                BoxDbMultiConditionOperator.Or,
                BoxDbMultiConditionOperator.And
            };

            long count = DataTableManager.instance.GetDataTableRowsCount<TestData>(conditions, multiConditionOps);
            DataTableManager.instance.Dispose();
            Assert.Greater(count, 0L);
        }
    }
}