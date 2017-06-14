using NUnit.Framework;
using QuickUnity.Core.Security;
using QuickUnity.Utilities;
using System;

namespace QuickUnity.UnitTests.Core.Security
{
    /// <summary>
    /// Unit test cases for struct SecureDouble.
    /// </summary>
    [TestFixture]
    internal class SecureDoubleTests
    {
        /// <summary>
        /// Simple test.
        /// </summary>
        [Test]
        public void SimpleTest()
        {
            double source = GetRandomValue();
            SecureDouble result = new SecureDouble(source);
            double value = result.GetValue();
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to double.
        /// </summary>
        [Test]
        public void ImplicitConversionToDoubleTest()
        {
            double source = GetRandomValue();
            SecureDouble result = new SecureDouble(source);
            double value = result;
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to SecureDouble.
        /// </summary>
        [Test]
        public void ImplicitConversionToSecureDoubleTest()
        {
            double source = GetRandomValue();
            SecureDouble result = source;
            Assert.AreEqual(source, result.GetValue());
        }

        /// <summary>
        /// Test case for addition operation.
        /// </summary>
        [Test]
        public void OperatorAdditionTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            SecureDouble result = resultA + resultB;
            Assert.AreEqual(a + b, result.GetValue());
        }

        /// <summary>
        /// Test case for subtraction operation.
        /// </summary>
        [Test]
        public void OperatorSubtractionTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            SecureDouble result = resultA - resultB;
            Assert.AreEqual(a - b, result.GetValue());
        }

        /// <summary>
        /// Test case for multiplication operation.
        /// </summary>
        [Test]
        public void OperatorMultiplicationTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            SecureDouble result = resultA * resultB;
            Assert.AreEqual(a * b, result.GetValue());
        }

        /// <summary>
        /// Test case for division operation.
        /// </summary>
        [Test]
        public void OperatorDivisionTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            SecureDouble result = resultA / resultB;
            Assert.AreEqual(a / b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator modulus.
        /// </summary>
        [Test]
        public void OperatorModulusTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            SecureDouble result = resultA % resultB;
            Assert.AreEqual(a % b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator less than.
        /// </summary>
        [Test]
        public void OperatorLessThanTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA < resultB;
            Assert.AreEqual(a < b, result);
        }

        /// <summary>
        /// Test case for operator greater than.
        /// </summary>
        [Test]
        public void OperatorGreaterThanTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA > resultB;
            Assert.AreEqual(a > b, result);
        }

        /// <summary>
        /// Test case for operator less than or equal to.
        /// </summary>
        [Test]
        public void OperatorLessThanOrEqualToTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA <= resultB;
            Assert.AreEqual(a <= b, result);
        }

        /// <summary>
        /// Test case for operator greater than or equal to.
        /// </summary>
        [Test]
        public void OperatorGreatorThanOrEqualToTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA >= resultB;
            Assert.AreEqual(a >= b, result);
        }

        /// <summary>
        /// Test case for operator equality.
        /// </summary>
        [Test]
        public void OperatorEqualityTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA == resultB;
            Assert.AreEqual(a == b, result);
        }

        /// <summary>
        /// Test case for operator not equal.
        /// </summary>
        [Test]
        public void OperatorNotEqualTest()
        {
            double a = GetRandomValue();
            double b = GetRandomValue();
            SecureDouble resultA = new SecureDouble(a);
            SecureDouble resultB = new SecureDouble(b);
            bool result = resultA != resultB;
            Assert.AreEqual(a != b, result);
        }

        /// <summary>
        /// Gets the random double value.
        /// </summary>
        /// <returns>The random long value.</returns>
        private double GetRandomValue()
        {
            int seed = MathUtility.GetRandomSeed();
            Random rnd = new Random(seed);
            return rnd.NextDouble();
        }
    }
}