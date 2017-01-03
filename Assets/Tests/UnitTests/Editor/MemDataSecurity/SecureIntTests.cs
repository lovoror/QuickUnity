using NUnit.Framework;
using QuickUnity.Core.Security;
using QuickUnity.Utilities;
using System;

namespace QuickUnity.UnitTests
{
    /// <summary>
    /// Unit test cases for struct SecureInt.
    /// </summary>
    [TestFixture]
    [Category("SercureIntTests")]
    internal class SecureIntTests
    {
        /// <summary>
        /// Simple test.
        /// </summary>
        [Test]
        public void SimpleTest()
        {
            int source = GetRandomValue();
            SecureInt sint = new SecureInt(source);
            int value = sint.GetValue();
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to int.
        /// </summary>
        [Test]
        public void ImplicitConversionToIntTest()
        {
            int source = GetRandomValue();
            SecureInt sint = new SecureInt(source);
            int value = sint;
            Assert.AreEqual(source, value);
        }

        /// <summary>
        /// Test case for implicit conversion to SecureInt.
        /// </summary>
        [Test]
        public void ImplicitConversionToSecureIntTest()
        {
            int source = GetRandomValue();
            SecureInt sint = source;
            Assert.AreEqual(source, sint.GetValue());
        }

        /// <summary>
        /// Test case for addition operation.
        /// </summary>
        [Test]
        public void OperatorAdditionTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            SecureInt result = sintA + sintB;
            Assert.AreEqual(a + b, result.GetValue());
        }

        /// <summary>
        /// Test case for subtraction operation.
        /// </summary>
        [Test]
        public void OperatorSubtractionTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            SecureInt result = sintA - sintB;
            Assert.AreEqual(a - b, result.GetValue());
        }

        /// <summary>
        /// Test case for multiplication operation.
        /// </summary>
        [Test]
        public void OperatorMultiplicationTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            SecureInt result = sintA * sintB;
            Assert.AreEqual(a * b, result.GetValue());
        }

        /// <summary>
        /// Test case for division operation.
        /// </summary>
        [Test]
        public void OperatorDivisionTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            SecureInt result = sintA / sintB;
            Assert.AreEqual(a / b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator modulus.
        /// </summary>
        [Test]
        public void OperatorModulusTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            SecureInt result = sintA % sintB;
            Assert.AreEqual(a % b, result.GetValue());
        }

        /// <summary>
        /// Test case for operator less than.
        /// </summary>
        [Test]
        public void OperatorLessThanTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA < sintB;
            Assert.AreEqual(a < b, result);
        }

        /// <summary>
        /// Test case for operator greater than.
        /// </summary>
        [Test]
        public void OperatorGreaterThanTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA > sintB;
            Assert.AreEqual(a > b, result);
        }

        /// <summary>
        /// Test case for operator less than or equal to.
        /// </summary>
        [Test]
        public void OperatorLessThanOrEqualToTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA <= sintB;
            Assert.AreEqual(a <= b, result);
        }

        /// <summary>
        /// Test case for operator greater than or equal to.
        /// </summary>
        [Test]
        public void OperatorGreatorThanOrEqualToTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA >= sintB;
            Assert.AreEqual(a >= b, result);
        }

        /// <summary>
        /// Test case for operator equality.
        /// </summary>
        [Test]
        public void OperatorEqualityTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA == sintB;
            Assert.AreEqual(a == b, result);
        }

        /// <summary>
        /// Test case for operator not equal.
        /// </summary>
        [Test]
        public void OperatorNotEqualTest()
        {
            int a = GetRandomValue();
            int b = GetRandomValue();
            SecureInt sintA = new SecureInt(a);
            SecureInt sintB = new SecureInt(b);
            bool result = sintA != sintB;
            Assert.AreEqual(a != b, result);
        }

        /// <summary>
        /// Gets the random value.
        /// </summary>
        /// <returns>System.Int32 The random value.</returns>
        private int GetRandomValue()
        {
            int seed = MathUtility.GetRandomSeed();
            Random rnd = new Random(seed);
            return rnd.Next(int.MinValue, int.MaxValue);
        }
    }
}