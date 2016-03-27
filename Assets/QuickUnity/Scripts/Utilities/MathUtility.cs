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
using UnityEngine;

namespace QuickUnity.Utilities
{
    /// <summary>
    /// A utility class for doing math calculation. This class cannot be inherited.
    /// </summary>
    public static class MathUtility
    {
        /// <summary>
        /// Gets the reciprocal of a number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>System.Single.</returns>
        public static float GetReciprocal(float number)
        {
            return 1.0f / number;
        }

        /// <summary>
        /// Generate Gaussian Number.
        /// </summary>
        /// <returns>System.Single.</returns>
        public static float GenGaussianNumber()
        {
            float x1 = UnityEngine.Random.value;
            float x2 = UnityEngine.Random.value;

            if (x1 == 0.0f)
                x1 = 0.01f;

            return (float)(System.Math.Sqrt(-2.0 * System.Math.Log(x1)) * System.Math.Cos(2.0 * Mathf.PI * x2));
        }

        /// <summary>
        /// Determines whether the specified n is odd.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns><c>true</c> if the specified n is odd; otherwise, <c>false</c>.</returns>
        public static bool IsOdd(int n)
        {
            return Convert.ToBoolean(n & 1);
        }

        /// <summary>
        /// Determines whether the specified n is odd.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns><c>true</c> if the specified n is odd; otherwise, <c>false</c>.</returns>
        public static bool IsOdd(float n)
        {
            return Convert.ToBoolean(Mathf.FloorToInt(n) & 1);
        }

        /// <summary>
        /// Determines whether the float type a equals the float type b.
        /// </summary>
        /// <param name="a">float a.</param>
        /// <param name="b">float b.</param>
        /// <returns>System.Boolean.</returns>
        public static bool Equals(float a, float b)
        {
            if (a >= b - Mathf.Epsilon && a <= b + Mathf.Epsilon)
                return true;

            return false;
        }
    }
}