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

        private List<AsyncSocketClient> _workClients = new List<AsyncSocketClient>();

        public readonly int _port;
        public readonly int _backlog;

        #endregion

        #region ======= PROPERTIES =======

        public int Port { get => _port; }


        #endregion

        #region ======= DELEGATES / EVENTS =======

        public delegate void OnStartDelegate();
        public delegate void OnStopDelegate();
        public delegate void OnAcceptDelegate(AsyncSocketClient client);
        public delegate void OnReceiveDelegate(byte[] buffer);
        public delegate void OnSendDelegate();
        public delegate void OnErrorDelegate(Exception ex);

        public event OnStartDelegate OnStart;
        public event OnStopDelegate OnStop;
        public event OnAcceptDelegate OnAccept;
        public event OnReceiveDelegate OnReceive;
        public event OnSendDelegate OnSend;
        public event OnErrorDelegate OnError;

        #endregion

        #region ======= CONSTRUCTORS =======

        public AsyncSocketServer(int port, int backlog = 1000)
        {
            _port = port;
            _backlog = backlog;
        }

        #endregion

        #region ======= METHODS =======

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

        public void Start() => Listen();

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
                workClient.OnDisconnect += () => _workClients.Remove(workClient);

                _workClients.Add(workClient);

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
