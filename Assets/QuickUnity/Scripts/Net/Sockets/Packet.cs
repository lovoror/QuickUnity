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

using System.IO;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// A base <c>IPacket</c> implementation.
    /// </summary>
    public class Packet : IPacket
    {
        /// <summary>
        /// The data of packet.
        /// </summary>
        protected object m_data;

        /// <summary>
        /// Gets the data of packet.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return m_data; }
        }

        /// <summary>
        /// The bytes of data.
        /// </summary>
        protected byte[] m_bytes;

        /// <summary>
        /// Gets the bytes of data.
        /// </summary>
        /// <value>The bytes.</value>
        public byte[] bytes
        {
            get { return m_bytes; }
        }

        /// <summary>
        /// The stream of byte array.
        /// </summary>
        protected MemoryStream m_stream;

        /// <summary>
        /// Gets the stream of byte array.
        /// </summary>
        /// <value>The stream.</value>
        public MemoryStream stream
        {
            get { return m_stream; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public Packet(object data = null)
        {
            m_data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Packet"/> class.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public Packet(byte[] bytes = null)
        {
            m_bytes = bytes;
            m_stream = new MemoryStream(m_bytes);
        }
    }
}