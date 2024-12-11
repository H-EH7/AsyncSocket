using System;
using System.Net.Sockets;

namespace AsyncSocket
{
    public class AsyncSocketClient
    {
        #region ======= FIELDS =======

        private int _bufferSize;

        private Socket _client;

        #endregion

        #region ======= PROPERTIES =======

        public bool IsConnected => _client?.Connected ?? false;

        #endregion

        #region ======= DELEGATES / EVENTS =======

        public delegate void OnConnectDelegate();
        public delegate void OnDisconnectDelegate();
        public delegate void OnReceiveDelegate(byte[] buffer);
        public delegate void OnSendDelegate();
        public delegate void OnErrorDelegate(Exception ex);

        public event OnConnectDelegate OnConnect;
        public event OnDisconnectDelegate OnDisconnect;
        public event OnReceiveDelegate OnReceive;
        public event OnSendDelegate OnSend;
        public event OnErrorDelegate OnError;

        #endregion

        #region ======= CONSTRUCTORS =======

        public AsyncSocketClient(int bufferSize = 1024)
        {
            _bufferSize = bufferSize;
        }

        public AsyncSocketClient(Socket client)
        {
            _client = client;
            _bufferSize = client.ReceiveBufferSize;
        }

        #endregion

        #region ======= INNER CLASS =======

        private class ReceiveObject
        {
            public byte[] Buffer { get; }
            public Socket Socket { get; }

            public ReceiveObject(int bufferSize, Socket socket)
            {
                Buffer = new byte[bufferSize];
                Socket = socket;
            }
        }

        #endregion

        #region ======= METHODS =======

        public void Connect(string host, int port)
        {
            try
            {
                if (_client == null) _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _client.BeginConnect(host, port, new AsyncCallback(ConnectCallBack), _client);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        private void ConnectCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = asyncResult.AsyncState as Socket;
                client.EndConnect(asyncResult);

                OnConnect?.Invoke();

                Receive();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                _client?.BeginDisconnect(false, new AsyncCallback(DisconnectCallBack), _client);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex); 
            }
        }

        private void DisconnectCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = asyncResult.AsyncState as Socket;

                client.EndDisconnect(asyncResult);
                client.Close();

                _client = null;

                OnDisconnect?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        public void Close() => Disconnect();

        public void Receive()
        {
            try
            {
                ReceiveObject receiveObject = new ReceiveObject(_bufferSize, _client);

                _client.BeginReceive(receiveObject.Buffer, 0, _bufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallBack), receiveObject);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                ReceiveObject receiveObject = (ReceiveObject)asyncResult.AsyncState;
                int receivedSize = receiveObject.Socket.EndReceive(asyncResult);

                if (receivedSize > 0)
                    OnReceive?.Invoke(receiveObject.Buffer);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                _client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), _client);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        private void SendCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = asyncResult.AsyncState as Socket;
                client.EndSend(asyncResult);

                OnSend?.Invoke();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        #endregion
    }
}
