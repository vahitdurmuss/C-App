namespace WEBSOCKETBILDIRIM_U2
{
  partial class ServerForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.listviewMessages = new System.Windows.Forms.ListView();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.label3 = new System.Windows.Forms.Label();
      this.button_banIP = new System.Windows.Forms.Button();
      this.textBox_banIP = new System.Windows.Forms.TextBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.bannedClientsBox = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.connectedClientsBox = new System.Windows.Forms.ListBox();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.button1 = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Location = new System.Drawing.Point(2, 2);
      this.tabControl1.Multiline = true;
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(756, 404);
      this.tabControl1.TabIndex = 14;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.listviewMessages);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(748, 378);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // listviewMessages
      // 
      this.listviewMessages.Location = new System.Drawing.Point(3, 6);
      this.listviewMessages.Name = "listviewMessages";
      this.listviewMessages.Size = new System.Drawing.Size(745, 372);
      this.listviewMessages.TabIndex = 4;
      this.listviewMessages.UseCompatibleStateImageBehavior = false;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.label3);
      this.tabPage2.Controls.Add(this.button_banIP);
      this.tabPage2.Controls.Add(this.textBox_banIP);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(748, 378);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(226, 116);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(58, 13);
      this.label3.TabIndex = 12;
      this.label3.Text = "IP Address";
      // 
      // button_banIP
      // 
      this.button_banIP.Location = new System.Drawing.Point(318, 155);
      this.button_banIP.Name = "button_banIP";
      this.button_banIP.Size = new System.Drawing.Size(52, 23);
      this.button_banIP.TabIndex = 10;
      this.button_banIP.Text = "Block";
      this.button_banIP.UseVisualStyleBackColor = true;
      this.button_banIP.Click += new System.EventHandler(this.button_banIP_Click);
      // 
      // textBox_banIP
      // 
      this.textBox_banIP.Location = new System.Drawing.Point(290, 113);
      this.textBox_banIP.Name = "textBox_banIP";
      this.textBox_banIP.Size = new System.Drawing.Size(112, 20);
      this.textBox_banIP.TabIndex = 11;
      // 
      // tabPage3
      // 
      this.tabPage3.BackColor = System.Drawing.Color.Transparent;
      this.tabPage3.Controls.Add(this.bannedClientsBox);
      this.tabPage3.Controls.Add(this.label1);
      this.tabPage3.Controls.Add(this.label2);
      this.tabPage3.Controls.Add(this.connectedClientsBox);
      this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(748, 378);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "tabPage3";
      // 
      // bannedClientsBox
      // 
      this.bannedClientsBox.FormattingEnabled = true;
      this.bannedClientsBox.Location = new System.Drawing.Point(509, 75);
      this.bannedClientsBox.Name = "bannedClientsBox";
      this.bannedClientsBox.Size = new System.Drawing.Size(165, 238);
      this.bannedClientsBox.TabIndex = 3;
      this.bannedClientsBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bannedClientsBox_MouseClick);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(63, 43);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 13);
      this.label1.TabIndex = 8;
      this.label1.Text = "connected";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(564, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(45, 13);
      this.label2.TabIndex = 9;
      this.label2.Text = "blocked";
      // 
      // connectedClientsBox
      // 
      this.connectedClientsBox.FormattingEnabled = true;
      this.connectedClientsBox.Location = new System.Drawing.Point(32, 75);
      this.connectedClientsBox.Name = "connectedClientsBox";
      this.connectedClientsBox.Size = new System.Drawing.Size(158, 238);
      this.connectedClientsBox.TabIndex = 2;
      this.connectedClientsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.connectedClientsBox_MouseDoubleClick);
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.Text = "notifyIcon1";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(13, 421);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(114, 23);
      this.button1.TabIndex = 15;
      this.button1.Text = "Run Server";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.start_server_Click);
      // 
      // ServerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(761, 456);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.tabControl1);
      this.Name = "ServerForm";
      this.Text = "WEBSOCKET SERVER";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
      this.Load += new System.EventHandler(this.ServerForm_Load);
      this.Resize += new System.EventHandler(this.ServerForm_Resize);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    public System.Windows.Forms.ListView listviewMessages;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button button_banIP;
    private System.Windows.Forms.TextBox textBox_banIP;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.ListBox bannedClientsBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.ListBox connectedClientsBox;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.Button button1;

  }
}

