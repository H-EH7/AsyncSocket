using System;
using System.Net;
using System.Net.Sockets;

namespace AsyncSocket
{
    public class AsyncSocketServer
    {
        #region ======= FIELDS =======

        private Socket _listener;

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
        public delegate void OnErrorDelegate(Exception ex);

        public event OnStartDelegate OnStart;
        public event OnStopDelegate OnStop;
        public event OnAcceptDelegate OnAccept;
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

        private void Accept()
        {
            try
            {
                _listener.BeginAccept(new AsyncCallback(AcceptCallBack), _listener);
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

                OnAccept?.Invoke(new AsyncSocketClient(workSocket));

                Accept();
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex);
            }
        }

        #endregion
    }
}
