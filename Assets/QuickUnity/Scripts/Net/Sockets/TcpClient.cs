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

using System.Net.Sockets;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize TCP Socket network communication client.
    /// </summary>
    public class TcpClient : SocketClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Socket" /> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public TcpClient(string host, int port = 0, int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base(host, port, SocketType.Stream, ProtocolType.Tcp, sendBufferSize, receiveBufferSize)
        {
        }

        #region API

        /// <summary>
        /// Connects to socket server.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            if (m_socket != null)
                m_socket.LingerState = new LingerOption(true, 0);
        }

        #endregion API
    }
}