using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace AsyncSocket
{
    public class AsyncSocketServer
    {
        #region ======= FIELDS =======

        private Socket _listener;

        private readonly int _port;
        private readonly int _backlog;

        #endregion

        #region ======= PROPERTIES =======

        /// <summary>
        /// 서버의 리스닝 Port 번호
        /// </summary>
        public int Port { get => _port; }

        #endregion

        #region ======= DELEGATES / EVENTS =======

        public delegate void OnStartDelegate();
        public delegate void OnStopDelegate();
        public delegate void OnAcceptDelegate(AsyncSocketClient client);
        public delegate void OnReceiveDelegate(byte[] buffer);
        public delegate void OnSendDelegate();
        public delegate void OnErrorDelegate(Exception ex);

        /// <summary>
        /// 서버 시작 시 발생하는 이벤트
        /// </summary>
        public event OnStartDelegate OnStart;

        /// <summary>
        /// 서버 중지 시 발생하는 이벤트
        /// </summary>
        public event OnStopDelegate OnStop;

        /// <summary>
        /// Client Accept 시 발생하는 이벤트
        /// </summary>
        public event OnAcceptDelegate OnAccept;

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
        /// <param name="port">리스닝 Port 번호</param>
        /// <param name="backlog">연결 대기 큐의 최대 길이 (default: 1000)</param>
        public AsyncSocketServer(int port, int backlog = 1000)
        {
            _port = port;
            _backlog = backlog;
        }

        #endregion

        #region ======= METHODS =======

        /// <summary>
        /// 서버가 리슨을 시작한다.
        /// </summary>
        public void Listen()
        {
            try
            {
                _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _listener.Bind(new IPEndPoint(IPAddress.Any, _port));
                _listener.Listen(_backlog);

                OnStart?.Invoke();

                Accept();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        /// <summary>
        /// 서버가 리슨을 시작한다.
        /// </summary>
        public void Start() => Listen();

        /// <summary>
        /// 서버를 중지시킨다.
        /// </summary>
        public void Stop()
        {
            if (_listener != null)
            {
                try
                {
                    if (_listener.IsBound)
                        _listener.Close();

                    _listener = null;

                    OnStop?.Invoke();
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// 특정 Client에게 데이터를 송신한다.
        /// </summary>
        /// <param name="client">송신할 Client</param>
        /// <param name="data">송신할 데이터의 Byte 배열</param>
        public void Send(AsyncSocketClient client, byte[] data) => client.Send(data);

        private void Accept()
        {
            try
            {
                _listener?.BeginAccept(new AsyncCallback(AcceptCallBack), _listener);
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        private void AcceptCallBack(IAsyncResult asyncResult)
        {
            try
            {
                Socket listener = asyncResult.AsyncState as Socket;
                Socket workSocket = listener.EndAccept(asyncResult);

                AsyncSocketClient workClient = new AsyncSocketClient(workSocket);
                workClient.OnReceive += (buffer) => OnReceive?.Invoke(buffer);
                workClient.OnSend += () => OnSend?.Invoke();
                workClient.OnError += (ex) => OnError?.Invoke(ex);

                workClient.Receive();

                OnAccept?.Invoke(workClient);

                Accept();
            }
            catch (ObjectDisposedException ex)
            {
                // Accept 중에 Socket 닫혔을 경우
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        #endregion
    }
}
