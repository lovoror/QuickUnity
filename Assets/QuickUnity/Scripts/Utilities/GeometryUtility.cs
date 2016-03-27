﻿/*
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

using UnityEngine;

namespace QuickUnity.Utilities
{
    /// <summary>
    /// Utility class for common geometric functions.
    /// </summary>
    public static class GeometryUtility
    {
        /// <summary>
        /// Gets the angle between objet A and B.
        /// </summary>
        /// <param name="a">The position of A.</param>
        /// <param name="b">The position of B.</param>
        /// <returns></returns>
        public static float GetAngle(Vector3 a, Vector3 b)
        {
            return Mathf.Acos(Vector3.Dot(a.normalized, b.normalized)) * Mathf.Rad2Deg;
        }
    }
}