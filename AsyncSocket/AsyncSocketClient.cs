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

        /// <summary>
        /// Client의 연결 여부
        /// </summary>
        public bool IsConnected => _client?.Connected ?? false;

        #endregion

        #region ======= DELEGATES / EVENTS =======

        public delegate void OnConnectDelegate();
        public delegate void OnDisconnectDelegate();
        public delegate void OnReceiveDelegate(byte[] buffer);
        public delegate void OnSendDelegate();
        public delegate void OnErrorDelegate(Exception ex);

        /// <summary>
        /// 연결 시 발생하는 이벤트
        /// </summary>
        public event OnConnectDelegate OnConnect;

        /// <summary>
        /// 연결 해제 시 발생하는 이벤트
        /// </summary>
        public event OnDisconnectDelegate OnDisconnect;

        /// <summary>
        /// 데이터 수신 시 발생하는 이벤트
        /// </summary>
        public event OnReceiveDelegate OnReceive;

        /// <summary>
        /// 데이터 송신 시 발생하는 이벤트
        /// </summary>
        public event OnSendDelegate OnSend;

        /// <summary>
        /// 에러 발생 시 이벤트
        /// </summary>
        public event OnErrorDelegate OnError;

        #endregion

        #region ======= CONSTRUCTORS =======

        /// <summary>
        /// 기본 생성자
        /// </summary>
        /// <param name="bufferSize">버퍼 사이즈 (default: 1024)</param>
        public AsyncSocketClient(int bufferSize = 1024)
        {
            _bufferSize = bufferSize;
        }

        /// <summary>
        /// Socket 주입 생성자
        /// </summary>
        /// <param name="client">주입할 Socket</param>
        public AsyncSocketClient(Socket client)
        {
            _client = client;
            _bufferSize = client.ReceiveBufferSize;
        }

        #endregion

        #region ======= INNER CLASS =======

        /// <summary>
        /// 데이터 수신 시 State를 저장할 class
        /// </summary>
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

        /// <summary>
        /// Socket 연결을 시작한다.
        /// </summary>
        /// <param name="host">Host의 IP</param>
        /// <param name="port">Host의 Port 번호</param>
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

        /// <summary>
        /// Socket의 연결을 해제한다.
        /// </summary>
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

        /// <summary>
        /// Socket의 연결을 해제한다.
        /// </summary>
        public void Close() => Disconnect();

        /// <summary>
        /// 데이터를 수신한다.
        /// </summary>
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

                Receive();
            }
            catch (ObjectDisposedException ex)
            {
                // Receive 중에 Socket 닫혔을 경우
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        /// <summary>
        /// 데이터를 송신한다.
        /// </summary>
        /// <param name="data">송신할 데이터의 Byte 배열</param>
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
            catch (ObjectDisposedException ex)
            {
                // Send 중에 Socket 닫혔을 경우
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        #endregion
    }
}
