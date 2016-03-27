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

using QuickUnity.Events;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace QuickUnity.Net.Sockets
{
    /// <summary>
    /// Realize Socket network communication client.
    /// </summary>
    public class SocketClient : ThreadEventDispatcher
    {
        /// <summary>
        /// The sign of sending data.
        /// </summary>
        protected bool m_isSendingData = false;

        /// <summary>
        /// The packet packer.
        /// </summary>
        protected IPacketPacker m_packetPacker;

        /// <summary>
        /// Gets or sets the packet packer.
        /// </summary>
        /// <value>The packet packer.</value>
        public IPacketPacker packetPacker
        {
            get { return m_packetPacker; }
            set { m_packetPacker = value; }
        }

        /// <summary>
        /// The packet unpacker.
        /// </summary>
        protected IPacketUnpacker m_packetUnpacker;

        /// <summary>
        /// Gets or sets the packet unpacker.
        /// </summary>
        /// <value>The packet unpacker.</value>
        public IPacketUnpacker packetUnpacker
        {
            get { return m_packetUnpacker; }
            set { m_packetUnpacker = value; }
        }

        /// <summary>
        /// Whether set delay time when send packet data.
        /// </summary>
        protected bool m_noSendDelay = true;

        /// <summary>
        /// Gets or sets a value indicating whether [no send delay].
        /// </summary>
        /// <value><c>true</c> if [no send delay]; otherwise, <c>false</c>.</value>
        public bool noSendDelay
        {
            get { return m_noSendDelay; }
            set { m_noSendDelay = value; }
        }

        /// <summary>
        /// Delay time when send packet data.
        /// </summary>
        protected int m_sendDelayTime = 16;

        /// <summary>
        /// Gets or sets the send delay time.
        /// </summary>
        /// <value>The send delay time.</value>
        public int sendDelayTime
        {
            get { return m_sendDelayTime; }
            set { m_sendDelayTime = value; }
        }

        /// <summary>
        /// The send asynchronous callback.
        /// </summary>
        private AsyncCallback m_sendAsyncCallback;

        /// <summary>
        /// The receive asynchronous callback.
        /// </summary>
        private AsyncCallback m_receiveAsyncCallback;

        /// <summary>
        /// The send packet buffer.
        /// </summary>
        protected Queue m_sendPacketQueue;

        /// <summary>
        /// The read buffer of socket connection.
        /// </summary>
        protected MemoryStream m_readBuffer;

        /// <summary>
        /// The host address.
        /// </summary>
        protected string m_host;

        /// <summary>
        /// Gets or sets the host address.
        /// </summary>
        /// <value>The host.</value>
        public string host
        {
            get { return m_host; }
        }

        /// <summary>
        /// The port number.
        /// </summary>
        protected int m_port;

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>The port.</value>
        public int port
        {
            get { return m_port; }
        }

        /// <summary>
        /// The send buffer size.
        /// </summary>
        protected int m_sendBufferSize = 0;

        /// <summary>
        /// The receive buffer size.
        /// </summary>
        protected int m_receiveBufferSize = 0;

        /// <summary>
        /// The socket object.
        /// </summary>
        protected Socket m_socket = null;

        /// <summary>
        /// The bytes of socket received bytes.
        /// </summary>
        protected byte[] m_receivedBytes;

        /// <summary>
        /// The type of socket.
        /// </summary>
        protected SocketType m_socketType = SocketType.Unknown;

        /// <summary>
        /// The type of protocol.
        /// </summary>
        protected ProtocolType m_proto = ProtocolType.Unknown;

        /// <summary>
        /// The timeout of sending data.
        /// </summary>
        protected int m_sendTimeout = 1000;

        /// <summary>
        /// Gets or sets the timeout of sending data.
        /// </summary>
        /// <value>
        /// The timeout of sending data.
        /// </value>
        public int sendTimeout
        {
            get { return m_sendTimeout; }
            set
            {
                m_sendTimeout = value;

                if (m_socket != null)
                    m_socket.SendTimeout = value;
            }
        }

        /// <summary>
        /// The timeout of receiving data.
        /// </summary>
        protected int m_receiveTimeout = 1000;

        /// <summary>
        /// Gets or sets the timeout of receiving data.
        /// </summary>
        /// <value>
        /// The timeout of receiving data.
        /// </value>
        public int receiveTimeout
        {
            get { return m_receiveTimeout; }
            set
            {
                m_receiveTimeout = value;

                if (m_socket != null)
                    m_socket.ReceiveTimeout = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TcpClient"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool connected
        {
            get
            {
                if (m_socket != null)
                    return m_socket.Connected;

                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketClient" /> class.
        /// </summary>
        /// <param name="host">The host address.</param>
        /// <param name="port">The port number.</param>
        /// <param name="socketType">Type of the socket.</param>
        /// <param name="proto">The proto type.</param>
        /// <param name="sendBufferSize">Size of the send buffer.</param>
        /// <param name="receiveBufferSize">Size of the receive buffer.</param>
        public SocketClient(string host, int port, SocketType socketType, ProtocolType proto = ProtocolType.Tcp,
            int sendBufferSize = 65536, int receiveBufferSize = 65536)
            : base()
        {
            m_host = host;
            m_port = port;
            m_socketType = socketType;
            m_proto = proto;

            m_sendBufferSize = sendBufferSize;
            m_receiveBufferSize = receiveBufferSize;

            // Initialize received byte array.
            m_receivedBytes = new byte[m_receiveBufferSize];

            // Initialize send and receive data callback.
            m_sendAsyncCallback = new AsyncCallback(SendDataAsync);
            m_receiveAsyncCallback = new AsyncCallback(ReceiveDataAsync);

            // Initialize read buffer and write buffer.
            m_readBuffer = new MemoryStream(m_sendBufferSize);

            // Initialize send packet buffer.
            m_sendPacketQueue = new Queue();
        }

        #region API

        /// <summary>
        /// Connects to socket server.
        /// </summary>
        public virtual void Connect()
        {
            if (!string.IsNullOrEmpty(m_host))
            {
                // Get host related information.
                IPHostEntry hostEntry = Dns.GetHostEntry(m_host);

                // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
                // an exception that occurs when the host IP Address is not compatible with the address family
                // (typical in the IPv6 case).
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    IPEndPoint endPoint = new IPEndPoint(address, m_port);
                    Socket tempSocket = new Socket(endPoint.AddressFamily, m_socketType, m_proto);
                    tempSocket.Ttl = 42;
                    tempSocket.Connect(endPoint);

                    if (tempSocket.Connected)
                    {
                        m_socket = tempSocket;
                        m_socket.SendTimeout = m_sendTimeout;
                        m_socket.ReceiveTimeout = m_receiveTimeout;
                        m_socket.SendBufferSize = m_sendBufferSize;
                        m_socket.ReceiveBufferSize = m_receiveBufferSize;
                        DispatchEvent(new SocketEvent(SocketEvent.Connected, this));
                        BeginReceive();
                        BeginSend();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Sends data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Send(object data)
        {
            if (data == null)
                return;

            if (m_packetPacker != null)
            {
                IPacket packet = m_packetPacker.Pack(data);

                if (packet != null && m_sendPacketQueue != null)
                    m_sendPacketQueue.Enqueue(packet);
            }

            if (!m_isSendingData)
                BeginSend();
        }

        /// <summary>
        /// Closes socket connect.
        /// </summary>
        public virtual void Close()
        {
            if (m_socket != null)
            {
                if (connected)
                {
                    try
                    {
                        m_socket.Shutdown(SocketShutdown.Both);
                    }
                    catch (Exception exception)
                    {
                        DispatchErrorEvent("TcpClient.Close() Error: " + exception.Message + exception.StackTrace);
                    }
                }

                m_socket.Close();
                DispatchEvent(new SocketEvent(SocketEvent.Closed, this));
            }
        }

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Sends the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected void SendDataAsync(IAsyncResult ar)
        {
            if (m_socket != null && connected)
            {
                try
                {
                    SocketError errorCode;
                    m_socket.EndSend(ar, out errorCode);
                }
                catch (Exception exception)
                {
                    DispatchErrorEvent("TcpClient.SendDataAsync() Error: " + exception.Message + exception.StackTrace);
                }

                // Send packet after delay time.
                if (!m_noSendDelay)
                    Thread.Sleep(m_sendDelayTime);

                m_isSendingData = false;
                BeginSend();
            }
        }

        /// <summary>
        /// Receives the data asynchronously.
        /// </summary>
        /// <param name="ar">The ar.</param>
        protected void ReceiveDataAsync(IAsyncResult ar)
        {
            if (m_socket != null && connected)
            {
                try
                {
                    SocketError errorCode;
                    int bytesRead = m_socket.EndReceive(ar, out errorCode);
                    m_readBuffer.Write(m_receivedBytes, 0, bytesRead);
                    UnpackPacket(bytesRead);
                    BeginReceive();
                }
                catch (Exception exception)
                {
                    DispatchErrorEvent("TcpClient.ReceiveDataAsync() Error: " + exception.Message + exception.StackTrace);
                }
            }
        }

        /// <summary>
        /// Begins send data asynchronously.
        /// </summary>
        protected void BeginSend()
        {
            if (m_socket != null && connected)
            {
                if (m_sendPacketQueue != null && m_sendPacketQueue.Count > 0)
                {
                    try
                    {
                        IPacket packet = (IPacket)m_sendPacketQueue.Dequeue();

                        if (packet != null && packet.bytes != null)
                        {
                            m_isSendingData = true;

                            m_socket.BeginSend(packet.bytes, 0, packet.bytes.Length, SocketFlags.None, m_sendAsyncCallback, m_socket);
                        }
                    }
                    catch (Exception exception)
                    {
                        DispatchErrorEvent("TcpClient.BeginSend() Error: " + exception.Message + exception.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// Begins receive data asynchronously.
        /// </summary>
        protected void BeginReceive()
        {
            try
            {
                if (m_socket != null && connected)
                    m_socket.BeginReceive(m_receivedBytes, 0, m_receivedBytes.Length, SocketFlags.None, m_receiveAsyncCallback, m_socket);
            }
            catch (Exception exception)
            {
                DispatchErrorEvent("TcpClient.BeginReceive() Error: " + exception.Message + exception.StackTrace);
            }
        }

        /// <summary>
        /// Unpacks the packet.
        /// </summary>
        protected void UnpackPacket(int bytesRead)
        {
            if (m_packetUnpacker != null)
            {
                IPacket[] packets = m_packetUnpacker.Unpack(m_readBuffer, bytesRead);

                if (packets != null && packets.Length > 0)
                {
                    foreach (IPacket packet in packets)
                        DispatchEvent(new SocketEvent(SocketEvent.Data, this, packet));
                }
            }
        }

        /// <summary>
        /// Dispatches the error event.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        protected void DispatchErrorEvent(string errorMessage)
        {
            SocketEvent evt = new SocketEvent(SocketEvent.Error, this);
            evt.errorMessage = errorMessage;
            DispatchEvent(evt);
        }

        #endregion Protected Functions
    }
}