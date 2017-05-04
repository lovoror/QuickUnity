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
    /// Represents float value that should be protected.
    /// </summary>
    public struct SecureFloat : IComparable<SecureFloat>
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
        /// Initializes a new instance of the <see cref="SecureFloat"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public SecureFloat(float value)
        {
            m_value = MemDataSecurity.EncryptFloatValue(value, out m_check);
        }

        /// <summary>
        /// Gets the original float value.
        /// </summary>
        /// <returns>The original float value.</returns>
        public float GetValue()
        {
            return MemDataSecurity.DecryptFloatValue(m_value, m_check);
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

            SecureFloat result = (SecureFloat)obj;

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
        public int CompareTo(SecureFloat other)
        {
            return m_value.CompareTo(other.GetValue());
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureFloat"/> to <see cref="System.Single"/>.
        /// </summary>
        /// <param name="value">The value of SecureFloat.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator float(SecureFloat value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureFloat"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecureFloat value)
        {
            double result = value.GetValue();
            return result;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Single"/> to <see cref="SecureFloat"/>.
        /// </summary>
        /// <param name="value">The float value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureFloat(float value)
        {
            return new SecureFloat(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="SecureFloat"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureFloat(SecureInt value)
        {
            return new SecureFloat(value.GetValue());
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureLong"/> to <see cref="SecureFloat"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureFloat(SecureLong value)
        {
            return new SecureFloat(value.GetValue());
        }

        /// <summary>
        /// Implements the operator ++.
        /// </summary>
        /// <param name="value">The SecureFloat value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator ++(SecureFloat value)
        {
            SecureFloat result = new SecureFloat(value + 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator --.
        /// </summary>
        /// <param name="value">The SecureFloat value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator --(SecureFloat value)
        {
            SecureFloat result = new SecureFloat(value - 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator +(SecureFloat a, SecureFloat b)
        {
            float result = a.GetValue() + b.GetValue();
            return new SecureFloat(result);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator -(SecureFloat a, SecureFloat b)
        {
            float result = a.GetValue() - b.GetValue();
            return new SecureFloat(result);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator *(SecureFloat a, SecureFloat b)
        {
            float result = a.GetValue() * b.GetValue();
            return new SecureFloat(result);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator /(SecureFloat a, SecureFloat b)
        {
            float result = a.GetValue() / b.GetValue();
            return new SecureFloat(result);
        }

        /// <summary>
        /// Implements the operator %.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureFloat operator %(SecureFloat a, SecureFloat b)
        {
            float result = a.GetValue() % b.GetValue();
            return new SecureFloat(result);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() < b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() > b.GetValue();
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() <= b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() >= b.GetValue();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() == b.GetValue();
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The SecureFloat object a.</param>
        /// <param name="b">The SecureFloat object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SecureFloat a, SecureFloat b)
        {
            return a.GetValue() != b.GetValue();
        }
    }
}