using DFBot.Network.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DFBot.Network
{
    // State object for reading client data asynchronously
    class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class SocketDof
    {
        /// <summary>
        /// Ip of the server
        /// </summary>
        private string _ipAdress;

        public string IpAddress
        {
            set { _ipAdress = value; }
        }

        /// <summary>
        /// Port of the server
        /// </summary>
        private int _port;

        /// <summary>
        /// socket object
        /// </summary>
        private Socket _clientSocket;

        /// <summary>
        /// Tool to build or parse network message
        /// </summary>
        private MessageProcess _messageProcessor;

        // ManualResetEvent instances signal completion.
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;

        private Thread receiveLoop;

        public SocketDof(string ipAdress, int port, Bot bot)
        {
            _ipAdress = ipAdress;
            _port = port;
            bot.Socket = this;
            _messageProcessor = new MessageProcess(bot);
        }



        #region Connection
        public bool IsConnected()
        {
            return connectDone.WaitOne(0);
        }

        /**
         * Create the socket
         **/
        public void SocketConnection()
        {
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(_ipAdress), _port);

                // Create a TCP/IP socket.
                _clientSocket = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.
                _clientSocket.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), _clientSocket);
                connectDone.WaitOne();

                receiveLoop = new Thread(new ThreadStart(ReceiveLoop));

                receiveLoop.Start();
            }
            catch (Exception e)
            {
                Program.log.Error(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Complete the connection.
                _clientSocket.EndConnect(ar);

                Program.log.Info("Socket connected to "+
                    _clientSocket.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Program.log.Error(e.ToString());
            }
        }

        public void ReconnectTo(string newIp)
        {
            SocketDeconnection();

            _ipAdress = newIp;

            SocketConnection();
        }
        #endregion

        #region Deconnection    
        /**
         * Release the socket
         * */
        public void SocketDeconnection()
        {
            //Stop receiving thread
            receiveLoop.Abort();
            
            //Release the socket.
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();

            Program.log.Info("Socket disconnected.");

            connectDone.Reset();
        }
        #endregion

        #region Send
        public void Send(String data)
        {
            while (!IsConnected())
            {
                Program.log.Info("Waiting connection to " +_ipAdress);
            }
            Program.log.Info("Trying to send data: " + data);

            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            _clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), _clientSocket);

            sendDone.WaitOne();
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = _clientSocket.EndSend(ar);
                Program.log.Info("Sent "+bytesSent+ "bytes to server.");

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendUnderSSL(string message)
        {
            Stream networkStream = new NetworkStream(_clientSocket);
            SslStream sslStream = new SslStream(networkStream);

            byte[] messageByte = Encoding.ASCII.GetBytes(message);
            sslStream.Write(messageByte);
        }
        #endregion

        #region receiver
        public void Receive()
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = _clientSocket;

                // Begin receiving the data from the remote device.
                _clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    
                    // Get the rest of the data.
                    if (bytesRead == StateObject.BufferSize){

                        Program.log.Info("Received not complete.");

                        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);

                        return;
                    }
                }
                
                // All the data has arrived; put it in response.
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                    Program.log.Info("Response: " + response);
                    Task a = Task.Factory.StartNew(() => _messageProcessor.ProcessReceivedMessage(response));
                    a.Wait();

                    // Signal that all bytes have been received.
                    receiveDone.Set();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

        private void ReceiveLoop()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                receiveDone.Reset();

                Program.log.Info("Receiving>>>");
                Receive();

                receiveDone.WaitOne();
            }
        }
    }
}
