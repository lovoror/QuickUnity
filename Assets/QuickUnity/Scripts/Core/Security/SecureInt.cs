/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System;

namespace QuickUnity.Core.Security
{
    /// <summary>
    /// Represents int value that should be protected.
    /// </summary>
    public struct SecureInt : IComparable<SecureInt>
    {
        /// <summary>
        /// The encrypted int value.
        /// </summary>
        private int m_value;

        /// <summary>
        /// The check value.
        /// </summary>
        private int m_check;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureInt"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public SecureInt(int value)
        {
            m_value = MemDataSecurity.EncryptIntValue(value, out m_check);
        }

        /// <summary>
        /// Gets the original int value.
        /// </summary>
        /// <returns>The original int value.</returns>
        public int GetValue()
        {
            return MemDataSecurity.DecryptIntValue(m_value, m_check);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            SecureInt result = (SecureInt)obj;

            if (result == this)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return GetValue().ToString();
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same
        /// position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(SecureInt other)
        {
            return m_value.CompareTo(other.GetValue());
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(SecureInt value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator long(SecureInt value)
        {
            long result = value.GetValue();
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="System.Single"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(SecureInt value)
        {
            float result = value.GetValue();
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecureInt value)
        {
            double result = value.GetValue();
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="SecureInt"/>.
        /// </summary>
        /// <param name="value">The int value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureInt(int value)
        {
            return new SecureInt(value);
        }

        /// <summary>
        /// Implements the operator ++.
        /// </summary>
        /// <param name="value">The SecureInt value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator ++(SecureInt value)
        {
            SecureInt result = new SecureInt(value + 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator --.
        /// </summary>
        /// <param name="value">The SecureInt value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator --(SecureInt value)
        {
            SecureInt result = new SecureInt(value - 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator +(SecureInt a, SecureInt b)
        {
            int result = a.GetValue() + b.GetValue();
            return new SecureInt(result);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator -(SecureInt a, SecureInt b)
        {
            int result = a.GetValue() - b.GetValue();
            return new SecureInt(result);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator *(SecureInt a, SecureInt b)
        {
            int result = a.GetValue() * b.GetValue();
            return new SecureInt(result);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator /(SecureInt a, SecureInt b)
        {
            int result = a.GetValue() / b.GetValue();
            return new SecureInt(result);
        }

        /// <summary>
        /// Implements the operator %.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureInt operator %(SecureInt a, SecureInt b)
        {
            int result = a.GetValue() % b.GetValue();
            return new SecureInt(result);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(SecureInt a, SecureInt b)
        {
            return a.GetValue() < b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(SecureInt a, SecureInt b)
        {
            return a.GetValue() > b.GetValue();
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(SecureInt a, SecureInt b)
        {
            return a.GetValue() <= b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(SecureInt a, SecureInt b)
        {
            return a.GetValue() >= b.GetValue();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SecureInt a, SecureInt b)
        {
            return a.GetValue() == b.GetValue();
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The SecureInt object a.</param>
        /// <param name="b">The SecureInt object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SecureInt a, SecureInt b)
        {
            return a.GetValue() != b.GetValue();
        }
    }
}