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

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// When you use socket, socket will dispatch SocketEvent.
    /// </summary>
    public class SocketEvent : Events.Event
    {
        /// <summary>
        /// When socket was connected to server, it will dispatch Connected event.
        /// </summary>
        public const string Connected = "connected";

        /// <summary>
        /// When socket received data from server, it will dispatch Data event after unpacker unpack packet.
        /// </summary>
        public const string Data = "data";

        /// <summary>
        /// When socket was closed, it will dispatch Closed event.
        /// </summary>
        public const string Closed = "closed";

        /// <summary>
        /// When socket got error, it will dispatch Error event.
        /// </summary>
        public const string Error = "Error";

        /// <summary>
        /// The packet data.
        /// </summary>
        private IPacket m_packet;

        /// <summary>
        /// Gets the packet.
        /// </summary>
        /// <value>The packet.</value>
        public IPacket packet
        {
            get { return m_packet; }
        }

        /// <summary>
        /// The error message.
        /// </summary>
        private string m_errorMessage;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string errorMessage
        {
            get { return m_errorMessage; }
            set { m_errorMessage = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="packet">The packet.</param>
        public SocketEvent(string type, object target = null, IPacket packet = null)
            : base(type, target)
        {
            m_packet = packet;
        }
    }
}