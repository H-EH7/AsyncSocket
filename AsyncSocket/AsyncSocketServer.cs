using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSocket
{
    public class AsyncSocketServer
    {
        #region ======= FIELDS =======

        private Socket _socket;

        public readonly int _port;

        #endregion

        #region ======= PROPERTIES =======

        public int Port { get => _port; }

        #endregion

        #region ======= DELEGATES / EVENTS =======

        public delegate void OnStartDelegate();
        public delegate void OnStopDelegate();
        public delegate void OnAcceptDelegate();
        public delegate void OnErrorDelegate();

        public event OnStartDelegate OnStart;
        public event OnStopDelegate OnStop;
        public event OnAcceptDelegate OnAccept;
        public event OnErrorDelegate OnError;

        #endregion

        #region ======= CONSTRUCTOR =======

        public AsyncSocketServer(int port) => _port = port;

        #endregion

        #region ======= METHODS =======



        #endregion
    }
}
