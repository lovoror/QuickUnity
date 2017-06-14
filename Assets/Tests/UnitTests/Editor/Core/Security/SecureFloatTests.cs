using NUnit.Framework;
using QuickUnity.Core.Security;
using QuickUnity.Utilities;
using UnityEngine;

namespace QuickUnity.UnitTests.Core.Security
{
    /// <summary>
    /// Unit test cases for struct SecureFloat.
    /// </summary>
    [TestFixture]
    internal class SecureFloatTests
    {
        /// <summary>
        /// Simple test.
        /// </summary>
        [Test]
        public void SimpleTest()
        {
            float source = GetRandomValue();
            SecureFloat sfloat = new SecureFloat(source);
            float value = sfloat.GetValue();
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to float.
        /// </summary>
        [Test]
        public void ImplicitConversionToFloatTest()
        {
            float source = GetRandomValue();
            SecureFloat result = new SecureFloat(source);
            float value = result;
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to SecureFloat.
        /// </summary>
        [Test]
        public void ImplicitConversionToSecureFloatTest()
        {
            float source = GetRandomValue();
            SecureFloat result = source;
            Assert.AreEqual(source, result.GetValue());
        }

        /// <summary>
        /// Test case for addition operation.
        /// </summary>
        [Test]
        public void OperatorAdditionTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            SecureFloat result = resultA + resultB;
            Assert.AreEqual(a + b, result.GetValue());
        }

        /// <summary>
        /// Test case for subtraction operation.
        /// </summary>
        [Test]
        public void OperatorSubtractionTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            SecureFloat result = resultA - resultB;
            Assert.AreEqual(a - b, result.GetValue());
        }

        /// <summary>
        /// Test case for multiplication operation.
        /// </summary>
        [Test]
        public void OperatorMultiplicationTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            SecureFloat result = resultA * resultB;
            Assert.AreEqual(a * b, result.GetValue());
        }

        /// <summary>
        /// Test case for division operation.
        /// </summary>
        [Test]
        public void OperatorDivisionTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            SecureFloat result = resultA / resultB;
            Assert.AreEqual(a / b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator modulus.
        /// </summary>
        [Test]
        public void OperatorModulusTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            SecureFloat result = resultA % resultB;
            Assert.AreEqual(a % b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator less than.
        /// </summary>
        [Test]
        public void OperatorLessThanTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA < resultB;
            Assert.AreEqual(a < b, result);
        }

        /// <summary>
        /// Test case for operator greater than.
        /// </summary>
        [Test]
        public void OperatorGreaterThanTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA > resultB;
            Assert.AreEqual(a > b, result);
        }

        /// <summary>
        /// Test case for operator less than or equal to.
        /// </summary>
        [Test]
        public void OperatorLessThanOrEqualToTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA <= resultB;
            Assert.AreEqual(a <= b, result);
        }

        /// <summary>
        /// Test case for operator greater than or equal to.
        /// </summary>
        [Test]
        public void OperatorGreatorThanOrEqualToTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA >= resultB;
            Assert.AreEqual(a >= b, result);
        }

        /// <summary>
        /// Test case for operator equality.
        /// </summary>
        [Test]
        public void OperatorEqualityTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA == resultB;
            Assert.AreEqual(a == b, result);
        }

        /// <summary>
        /// Test case for operator not equal.
        /// </summary>
        [Test]
        public void OperatorNotEqualTest()
        {
            float a = GetRandomValue();
            float b = GetRandomValue();
            SecureFloat resultA = new SecureFloat(a);
            SecureFloat resultB = new SecureFloat(b);
            bool result = resultA != resultB;
            Assert.AreEqual(a != b, result);
        }

        /// <summary>
        /// Gets the random float value.
        /// </summary>
        /// <returns>The random float value.</returns>
        private float GetRandomValue()
        {
            Random.InitState(MathUtility.GetRandomSeed());
            return Random.Range(float.MinValue, float.MaxValue);
        }
    }
}