/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2016 Jerry Lee
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
    /// Represents double value that should be protected.
    /// </summary>
    public struct SecureDouble
    {
        /// <summary>
        /// The encrypted long value.
        /// </summary>
        private long m_value;

        /// <summary>
        /// The check value.
        /// </summary>
        private long m_check;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureDouble"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public SecureDouble(double value)
        {
            m_value = MemDataSecurity.EncryptDoubleValue(value, out m_check);
        }

        /// <summary>
        /// Gets the original double value.
        /// </summary>
        /// <returns>System.Double The original double value.</returns>
        public double GetValue()
        {
            return MemDataSecurity.DecryptDoubleValue(m_value, m_check);
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

            SecureDouble result = (SecureDouble)obj;

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
        /// Performs an implicit conversion from <see cref="SecureDouble"/> to <see cref="System.Double"/>.
        /// </summary>
        /// <param name="value">The value of SecureDouble.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator double(SecureDouble value)
        {
            return value.GetValue();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Double"/> to <see cref="SecureDouble"/>.
        /// </summary>
        /// <param name="value">The double value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureDouble(double value)
        {
            return new SecureDouble(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureInt"/> to <see cref="SecureDouble"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureDouble(SecureInt value)
        {
            return new SecureDouble(value.GetValue());
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureLong"/> to <see cref="SecureDouble"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureDouble(SecureLong value)
        {
            return new SecureDouble(value.GetValue());
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SecureFloat"/> to <see cref="SecureDouble"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator SecureDouble(SecureFloat value)
        {
            return new SecureDouble(value.GetValue());
        }

        /// <summary>
        /// Implements the operator ++.
        /// </summary>
        /// <param name="value">The SecureDouble value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator ++(SecureDouble value)
        {
            SecureDouble result = new SecureDouble(value + 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator --.
        /// </summary>
        /// <param name="value">The SecureDouble value.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator --(SecureDouble value)
        {
            SecureDouble result = new SecureDouble(value - 1);
            value.m_check = result.m_check;
            value.m_value = result.m_value;
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator +(SecureDouble a, SecureDouble b)
        {
            double result = a.GetValue() + b.GetValue();
            return new SecureDouble(result);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator -(SecureDouble a, SecureDouble b)
        {
            double result = a.GetValue() - b.GetValue();
            return new SecureDouble(result);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator *(SecureDouble a, SecureDouble b)
        {
            double result = a.GetValue() * b.GetValue();
            return new SecureDouble(result);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator /(SecureDouble a, SecureDouble b)
        {
            double result = a.GetValue() / b.GetValue();
            return new SecureDouble(result);
        }

        /// <summary>
        /// Implements the operator %.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static SecureDouble operator %(SecureDouble a, SecureDouble b)
        {
            double result = a.GetValue() % b.GetValue();
            return new SecureDouble(result);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() < b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() > b.GetValue();
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() <= b.GetValue();
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() >= b.GetValue();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() == b.GetValue();
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">The SecureDouble object a.</param>
        /// <param name="b">The SecureDouble object b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(SecureDouble a, SecureDouble b)
        {
            return a.GetValue() != b.GetValue();
        }
    }
}