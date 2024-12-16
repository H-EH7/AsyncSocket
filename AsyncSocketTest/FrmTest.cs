using AsyncSocket;
using System;
using System.Text;
using System.Windows.Forms;

namespace AsyncSocketTest
{
    public partial class FrmTest : Form
    {
        private AsyncSocketServer _server = new AsyncSocketServer(502);
        private AsyncSocketClient _serverWorkSocket;

        private AsyncSocketClient _client = new AsyncSocketClient();

        public FrmTest()
        {
            InitializeComponent();

            InitServer();
            InitClient();
        }

        private void InitServer()
        {
            _server.OnStart += _server_OnStart;
            _server.OnStop += _server_OnStop;
            _server.OnAccept += _server_OnAccept;
            _server.OnReceive += _server_OnReceive;
            _server.OnSend += _server_OnSend;
            _server.OnError += _server_OnError;
        }


        private void InitClient()
        {
            _client.OnConnect += _client_OnConnect;
            _client.OnDisconnect += _client_OnDisconnect;
            _client.OnReceive += _client_OnReceive;
            _client.OnSend += _client_OnSend;
            _client.OnError += _client_OnError;
        }

        private void Log(TextBox textBox, string msg)
        {
            textBox.AppendText($"[{DateTime.Now.ToString("HH:mm:ss").PadLeft(8)}] {msg}\r\n");
        }

        #region SERVER EVENTS

        private void _server_OnStart()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, "Server Start.");
            }));
        }

        private void _server_OnStop()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, "Server Stop.");
            }));
        }

        private void _server_OnAccept(AsyncSocketClient client)
        {
            _serverWorkSocket = client;

            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, "Client Accepted.");
            }));

            byte[] msg = Encoding.UTF8.GetBytes("Accept.");
            _serverWorkSocket.Send(msg);
        }

        private void _server_OnReceive(byte[] buffer)
        {
            string msg = Encoding.UTF8.GetString(buffer).TrimEnd('\0');

            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, $"Receive: {msg}");
            }));
        }

        private void _server_OnSend()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, "Server Send.");
            }));
        }

        private void _server_OnError(Exception ex)
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtServerLog, ex.Message);
            }));
        }

        #endregion

        #region CLIENT EVENTS

        private void _client_OnConnect()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtClientLog, "Client Connected.");
            }));
        }

        private void _client_OnDisconnect()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtClientLog, "Client Disconnected");
            }));
        }

        private void _client_OnReceive(byte[] buffer)
        {
            string msg = Encoding.UTF8.GetString(buffer).TrimEnd('\0');

            BeginInvoke(new Action(() =>
            {
                Log(txtClientLog, $"Receive: {msg}");
            }));
        }

        private void _client_OnSend()
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtClientLog, "Client Send Message.");
            }));
        }

        private void _client_OnError(Exception ex)
        {
            BeginInvoke(new Action(() =>
            {
                Log(txtClientLog, ex.Message);
            }));
        }

        #endregion

        #region ======= FORM EVENTS =======

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            _server.Start();
        }

        private void btnServerStop_Click(object sender, EventArgs e)
        {
            _server.Stop();
        }

        private void btnServerSend_Click(object sender, EventArgs e)
        {
            if (_serverWorkSocket is null) return;

            string msgStr = txtServerMessage.Text;
            byte[] msgByte = Encoding.UTF8.GetBytes(msgStr);

            _server.Send(_serverWorkSocket, msgByte);

            txtServerMessage.Text = "";
        }

        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            _client.Connect("127.0.0.1", 502);
        }

        private void btnClientDisconnect_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
        }

        private void btnClientSend_Click(object sender, EventArgs e)
        {
            string msgStr = txtClientMessage.Text;
            byte[] msgByte = Encoding.UTF8.GetBytes(msgStr);

            _client.Send(msgByte);

            txtClientMessage.Text = "";
        }

        #endregion
    }
}
