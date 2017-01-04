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

using QuickUnity.Utilities;
using System;

namespace QuickUnity.Core.Security
{
    /// <summary>
    /// MemDataSecurity is a class that process memory data encryption and decryption.
    /// </summary>
    internal sealed class MemDataSecurity
    {
        /// <summary>
        /// The key.
        /// </summary>
        private static int s_key;

        /// <summary>
        /// The long key.
        /// </summary>
        private static long s_longKey;

        /// <summary>
        /// The check key.
        /// </summary>
        private static int s_checkKey;

        /// <summary>
        /// The check long key.
        /// </summary>
        private static long s_checkLongKey;

        /// <summary>
        /// Initializes static members of the <see cref="MemDataSecurity"/> class.
        /// </summary>
        static MemDataSecurity()
        {
            int seed = MathUtility.GetRandomSeed();
            Random rnd = new Random(seed);
            int minValue = int.MinValue;
            int maxValue = int.MaxValue;
            s_key = rnd.Next(int.MinValue, int.MaxValue);
            s_longKey = ((long)s_key << 32) + s_key;
            s_checkKey = rnd.Next(minValue, maxValue);
            s_checkLongKey = ((long)s_checkKey << 32) + s_checkKey;
        }

        /// <summary>
        /// Encrypts the int value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int32 The encrypted int value.</returns>
        public static int EncryptIntValue(int value, out int check)
        {
            int result = (value ^ s_key);
            check = (value ^ s_checkKey);
            return result;
        }

        /// <summary>
        /// Decrypts the int value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int32 The decrypted int value.</returns>
        /// <exception cref="QuickUnity.Core.Security.MemDataModificationException">
        /// If the data has been modified.
        /// </exception>
        public static int DecryptIntValue(int value, int check)
        {
            int result = value ^ s_key;
            check ^= s_checkKey;

            if (result == check)
            {
                return result;
            }

            throw new MemDataModificationException();
        }

        /// <summary>
        /// Encrypts the float value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int32 The encrypted float value.</returns>
        public static int EncryptFloatValue(float value, out int check)
        {
            int result = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
            return EncryptIntValue(result, out check);
        }

        /// <summary>
        /// Decrypts the float value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Single The encrypted float value.</returns>
        public static float DecryptFloatValue(int value, int check)
        {
            int result = DecryptIntValue(value, check);
            return BitConverter.ToSingle(BitConverter.GetBytes(result), 0);
        }

        /// <summary>
        /// Encrypts the long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int64 The encrypted long value.</returns>
        public static long EncryptLongValue(long value, out long check)
        {
            long result = (value ^ s_longKey);
            check = (value ^ s_checkKey);
            return result;
        }

        /// <summary>
        /// Decrypts the long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int64 The decrypted long value.</returns>
        /// <exception cref="QuickUnity.Core.Security.MemDataModificationException">
        /// If the data has been modified.
        /// </exception>
        public static long DecryptLongValue(long value, long check)
        {
            long result = value ^ s_longKey;
            check ^= s_checkKey;

            if (result == check)
            {
                return result;
            }

            throw new MemDataModificationException();
        }

        /// <summary>
        /// Encrypts the double value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Int64 The encrypted double value.</returns>
        public static long EncryptDoubleValue(double value, out long check)
        {
            long result = BitConverter.DoubleToInt64Bits(value);
            return EncryptLongValue(result, out check);
        }

        /// <summary>
        /// Decrypts the double value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="check">The check.</param>
        /// <returns>System.Double The decrypted double value.</returns>
        public static double DecryptDoubleValue(long value, long check)
        {
            long result = DecryptLongValue(value, check);
            return BitConverter.Int64BitsToDouble(result);
        }
    }
}