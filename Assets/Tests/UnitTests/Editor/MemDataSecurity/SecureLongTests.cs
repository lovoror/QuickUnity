using NUnit.Framework;
using QuickUnity.Core.Security;
using QuickUnity.Utilities;
using UnityEngine;

namespace QuickUnity.UnitTests
{
    /// <summary>
    /// Unit test cases for struct SecureLong.
    /// </summary>
    [TestFixture]
    [Category("SercureLongTests")]
    internal class SecureLongTests
    {
        /// <summary>
        /// Simple test.
        /// </summary>
        [Test]
        public void SimpleTest()
        {
            long source = GetRandomValue();
            SecureLong result = new SecureLong(source);
            long value = result.GetValue();
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to long.
        /// </summary>
        [Test]
        public void ImplicitConversionToLongTest()
        {
            long source = GetRandomValue();
            SecureLong result = new SecureLong(source);
            long value = result;
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to SecureLong.
        /// </summary>
        [Test]
        public void ImplicitConversionToSecureLongTest()
        {
            long source = GetRandomValue();
            SecureLong result = source;
            Assert.AreEqual(source, result.GetValue());
        }

        /// <summary>
        /// Test case for addition operation.
        /// </summary>
        [Test]
        public void OperatorAdditionTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            SecureLong result = resultA + resultB;
            Assert.AreEqual(a + b, result.GetValue());
        }

        /// <summary>
        /// Test case for subtraction operation.
        /// </summary>
        [Test]
        public void OperatorSubtractionTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            SecureLong result = resultA - resultB;
            Assert.AreEqual(a - b, result.GetValue());
        }

        /// <summary>
        /// Test case for multiplication operation.
        /// </summary>
        [Test]
        public void OperatorMultiplicationTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            SecureLong result = resultA * resultB;
            Assert.AreEqual(a * b, result.GetValue());
        }

        /// <summary>
        /// Test case for division operation.
        /// </summary>
        [Test]
        public void OperatorDivisionTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            SecureLong result = resultA / resultB;
            Assert.AreEqual(a / b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator modulus.
        /// </summary>
        [Test]
        public void OperatorModulusTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            SecureLong result = resultA % resultB;
            Assert.AreEqual(a % b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator less than.
        /// </summary>
        [Test]
        public void OperatorLessThanTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA < resultB;
            Assert.AreEqual(a < b, result);
        }

        /// <summary>
        /// Test case for operator greater than.
        /// </summary>
        [Test]
        public void OperatorGreaterThanTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA > resultB;
            Assert.AreEqual(a > b, result);
        }

        /// <summary>
        /// Test case for operator less than or equal to.
        /// </summary>
        [Test]
        public void OperatorLessThanOrEqualToTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA <= resultB;
            Assert.AreEqual(a <= b, result);
        }

        /// <summary>
        /// Test case for operator greater than or equal to.
        /// </summary>
        [Test]
        public void OperatorGreatorThanOrEqualToTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA >= resultB;
            Assert.AreEqual(a >= b, result);
        }

        /// <summary>
        /// Test case for operator equality.
        /// </summary>
        [Test]
        public void OperatorEqualityTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA == resultB;
            Assert.AreEqual(a == b, result);
        }

        /// <summary>
        /// Test case for operator not equal.
        /// </summary>
        [Test]
        public void OperatorNotEqualTest()
        {
            long a = GetRandomValue();
            long b = GetRandomValue();
            SecureLong resultA = new SecureLong(a);
            SecureLong resultB = new SecureLong(b);
            bool result = resultA != resultB;
            Assert.AreEqual(a != b, result);
        }

        /// <summary>
        /// Gets the random long value.
        /// </summary>
        /// <returns>The random long value.</returns>
        private long GetRandomValue()
        {
            Random.InitState(MathUtility.GetRandomSeed());
            return (long)Random.Range(float.MinValue, float.MaxValue);
        }
    }
}