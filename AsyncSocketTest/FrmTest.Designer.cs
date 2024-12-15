namespace AsyncSocketTest
{
    partial class FrmTest
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbServer = new System.Windows.Forms.Label();
            this.btnServerStop = new System.Windows.Forms.Button();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.txtServerLog = new System.Windows.Forms.TextBox();
            this.txtClientLog = new System.Windows.Forms.TextBox();
            this.panelClient = new System.Windows.Forms.Panel();
            this.txtClientMessage = new System.Windows.Forms.TextBox();
            this.lbClientMessage = new System.Windows.Forms.Label();
            this.lbClient = new System.Windows.Forms.Label();
            this.btnClientDisconnect = new System.Windows.Forms.Button();
            this.btnClientSend = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.btnServerSend = new System.Windows.Forms.Button();
            this.lbServerMessage = new System.Windows.Forms.Label();
            this.txtServerMessage = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.txtServerLog, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.txtClientLog, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelClient, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtServerMessage);
            this.panel1.Controls.Add(this.lbServerMessage);
            this.panel1.Controls.Add(this.lbServer);
            this.panel1.Controls.Add(this.btnServerStop);
            this.panel1.Controls.Add(this.btnServerStart);
            this.panel1.Controls.Add(this.btnServerSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 144);
            this.panel1.TabIndex = 3;
            // 
            // lbServer
            // 
            this.lbServer.AutoSize = true;
            this.lbServer.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbServer.Location = new System.Drawing.Point(3, 6);
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(59, 16);
            this.lbServer.TabIndex = 2;
            this.lbServer.Text = "Server";
            // 
            // btnServerStop
            // 
            this.btnServerStop.Location = new System.Drawing.Point(287, 96);
            this.btnServerStop.Name = "btnServerStop";
            this.btnServerStop.Size = new System.Drawing.Size(80, 34);
            this.btnServerStop.TabIndex = 1;
            this.btnServerStop.Text = "Stop";
            this.btnServerStop.UseVisualStyleBackColor = true;
            this.btnServerStop.Click += new System.EventHandler(this.btnServerStop_Click);
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(201, 96);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(80, 34);
            this.btnServerStart.TabIndex = 1;
            this.btnServerStart.Text = "Start";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // txtServerLog
            // 
            this.txtServerLog.BackColor = System.Drawing.Color.Black;
            this.txtServerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServerLog.ForeColor = System.Drawing.Color.Lime;
            this.txtServerLog.Location = new System.Drawing.Point(3, 153);
            this.txtServerLog.Multiline = true;
            this.txtServerLog.Name = "txtServerLog";
            this.txtServerLog.ReadOnly = true;
            this.txtServerLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerLog.Size = new System.Drawing.Size(394, 294);
            this.txtServerLog.TabIndex = 0;
            // 
            // txtClientLog
            // 
            this.txtClientLog.BackColor = System.Drawing.Color.Black;
            this.txtClientLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClientLog.ForeColor = System.Drawing.Color.Lime;
            this.txtClientLog.Location = new System.Drawing.Point(403, 153);
            this.txtClientLog.Multiline = true;
            this.txtClientLog.Name = "txtClientLog";
            this.txtClientLog.ReadOnly = true;
            this.txtClientLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClientLog.Size = new System.Drawing.Size(394, 294);
            this.txtClientLog.TabIndex = 0;
            // 
            // panelClient
            // 
            this.panelClient.Controls.Add(this.txtClientMessage);
            this.panelClient.Controls.Add(this.lbClientMessage);
            this.panelClient.Controls.Add(this.lbClient);
            this.panelClient.Controls.Add(this.btnClientDisconnect);
            this.panelClient.Controls.Add(this.btnClientSend);
            this.panelClient.Controls.Add(this.btnClientConnect);
            this.panelClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClient.Location = new System.Drawing.Point(403, 3);
            this.panelClient.Name = "panelClient";
            this.panelClient.Size = new System.Drawing.Size(394, 144);
            this.panelClient.TabIndex = 2;
            // 
            // txtClientMessage
            // 
            this.txtClientMessage.Location = new System.Drawing.Point(26, 51);
            this.txtClientMessage.Name = "txtClientMessage";
            this.txtClientMessage.Size = new System.Drawing.Size(247, 21);
            this.txtClientMessage.TabIndex = 4;
            // 
            // lbClientMessage
            // 
            this.lbClientMessage.AutoSize = true;
            this.lbClientMessage.Location = new System.Drawing.Point(24, 36);
            this.lbClientMessage.Name = "lbClientMessage";
            this.lbClientMessage.Size = new System.Drawing.Size(58, 12);
            this.lbClientMessage.TabIndex = 3;
            this.lbClientMessage.Text = "Message";
            // 
            // lbClient
            // 
            this.lbClient.AutoSize = true;
            this.lbClient.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbClient.Location = new System.Drawing.Point(3, 6);
            this.lbClient.Name = "lbClient";
            this.lbClient.Size = new System.Drawing.Size(53, 16);
            this.lbClient.TabIndex = 2;
            this.lbClient.Text = "Client";
            // 
            // btnClientDisconnect
            // 
            this.btnClientDisconnect.Location = new System.Drawing.Point(286, 96);
            this.btnClientDisconnect.Name = "btnClientDisconnect";
            this.btnClientDisconnect.Size = new System.Drawing.Size(80, 34);
            this.btnClientDisconnect.TabIndex = 1;
            this.btnClientDisconnect.Text = "Disconnect";
            this.btnClientDisconnect.UseVisualStyleBackColor = true;
            this.btnClientDisconnect.Click += new System.EventHandler(this.btnClientDisconnect_Click);
            // 
            // btnClientSend
            // 
            this.btnClientSend.Location = new System.Drawing.Point(286, 43);
            this.btnClientSend.Name = "btnClientSend";
            this.btnClientSend.Size = new System.Drawing.Size(80, 34);
            this.btnClientSend.TabIndex = 1;
            this.btnClientSend.Text = "Send";
            this.btnClientSend.UseVisualStyleBackColor = true;
            this.btnClientSend.Click += new System.EventHandler(this.btnClientSend_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(200, 96);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(80, 34);
            this.btnClientConnect.TabIndex = 1;
            this.btnClientConnect.Text = "Connect";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // btnServerSend
            // 
            this.btnServerSend.Location = new System.Drawing.Point(287, 43);
            this.btnServerSend.Name = "btnServerSend";
            this.btnServerSend.Size = new System.Drawing.Size(80, 34);
            this.btnServerSend.TabIndex = 1;
            this.btnServerSend.Text = "Send";
            this.btnServerSend.UseVisualStyleBackColor = true;
            this.btnServerSend.Click += new System.EventHandler(this.btnServerSend_Click);
            // 
            // lbServerMessage
            // 
            this.lbServerMessage.AutoSize = true;
            this.lbServerMessage.Location = new System.Drawing.Point(25, 36);
            this.lbServerMessage.Name = "lbServerMessage";
            this.lbServerMessage.Size = new System.Drawing.Size(58, 12);
            this.lbServerMessage.TabIndex = 3;
            this.lbServerMessage.Text = "Message";
            // 
            // txtServerMessage
            // 
            this.txtServerMessage.Location = new System.Drawing.Point(27, 51);
            this.txtServerMessage.Name = "txtServerMessage";
            this.txtServerMessage.Size = new System.Drawing.Size(247, 21);
            this.txtServerMessage.TabIndex = 4;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "FrmTest";
            this.Text = "Test";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelClient.ResumeLayout(false);
            this.panelClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TextBox txtServerLog;
        private System.Windows.Forms.TextBox txtClientLog;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.Panel panelClient;
        private System.Windows.Forms.Label lbClient;
        private System.Windows.Forms.Button btnClientDisconnect;
        private System.Windows.Forms.Button btnClientSend;
        private System.Windows.Forms.TextBox txtClientMessage;
        private System.Windows.Forms.Label lbClientMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbServer;
        private System.Windows.Forms.Button btnServerStop;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.TextBox txtServerMessage;
        private System.Windows.Forms.Label lbServerMessage;
        private System.Windows.Forms.Button btnServerSend;
    }
}

