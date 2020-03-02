using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBSOCKETBILDIRIM_U2
{


  public partial class ServerForm : Form
  {

    Server server;
    Thread serverThread;
   
    #if DEBUG
    readonly int serverType = 2;
    #else
    readonly int serverType = 1;
    #endif

    public ServerForm()
    {
      InitializeComponent();
    }
    private void ServerForm_Load(object sender, EventArgs e)
    {
      initFormComponents();
    }
    private void ServerForm_Resize(object sender, EventArgs e)
    {
      if (WindowState == FormWindowState.Minimized)
      {
        this.Hide();
        ShowIcon = false;
        notifyIcon1.Visible = true;
        notifyIcon1.ShowBalloonTip(1000);
      }
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      ShowIcon = true;
      ShowInTaskbar = true;
      notifyIcon1.Visible = false;
      this.Show();
    }
    private void start_server_Click(object sender, EventArgs e)
    {
      if (serverThread == null)
      {
        serverThread = new Thread(new ThreadStart(serverThreadFunction));
        serverThread.Start();
      }
      else
        MessageBox.Show("Server is already running..");
    }
       
    private void button_banIP_Click(object sender, EventArgs e)
    {
      try
      {
        Regex regexIP = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");
        if (!String.IsNullOrEmpty(textBox_banIP.Text))

          if (regexIP.Match(textBox_banIP.Text).Success)
          {
            if (Server.bannedClientIPs.Contains(textBox_banIP.Text))
            {
              MessageBox.Show("IP Address you have entered before have been blocked.");
            }
            else
            {
              Server.bannedClientIPs.Add(textBox_banIP.Text);
              bannedClientsBox.Items.Add(textBox_banIP.Text);
              MessageBox.Show(textBox_banIP.Text + " Address has been blocked.");
              textBox_banIP.Text = "";
            }
          }
          else
            MessageBox.Show("Numbers you entered have not matched with computer standart network IP format. ");
        else
            MessageBox.Show("You entered empty value. IP address you want to block, enter into textbox ");

        if (Server.getConnectedClientList() != null)
        {
          connectedClientsBox.Items.Clear();
          foreach (string ip in Server.getConnectedClientList())
            connectedClientsBox.Items.Add(ip);
        }    

      }
      catch (Exception ex)
      {

      }
    }
    private void bannedClientsBox_MouseClick(object sender, MouseEventArgs e)
    {
      try
      {
        string selectedBannedClient = bannedClientsBox.SelectedItem.ToString();
        if (!String.IsNullOrEmpty(selectedBannedClient))
        {
          Server.bannedClientIPs.Remove(selectedBannedClient);
          bannedClientsBox.Items.Remove(bannedClientsBox.SelectedItem);
        }
        if (Server.getConnectedClientList() != null)
        {
          connectedClientsBox.Items.Clear();
          foreach (string ip in Server.getConnectedClientList())
            connectedClientsBox.Items.Add(ip);
        }    
      }
      catch
      {
        MessageBox.Show("for making unblock a client, double click on it.");
      }
    }
    private void connectedClientsBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      try
      {
        string selectedClient = connectedClientsBox.SelectedItem.ToString();
        if (!String.IsNullOrEmpty(selectedClient))
        {
          string[] ipV4 = selectedClient.Split(':');
          bannedClientsBox.Items.Add(ipV4[0]);
          Server.bannedClientIPs.Add(ipV4[0]);
          connectedClientsBox.Items.Remove(connectedClientsBox.SelectedItem);
        }
        else
          MessageBox.Show("to block a connected client, do double click on it.");       
      }
      catch
      {

      }
    }
    private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      Environment.Exit(Environment.ExitCode);
    }

    private void initFormComponents()
    {
      notifyIcon1.BalloonTipText = "running..";
      notifyIcon1.BalloonTipTitle = "WebSocket App";


      tabControl1.TabPages[0].Text = "Sent Messages";
      tabControl1.TabPages[1].Text = "Block User";
      tabControl1.TabPages[2].Text = "User Conditions on Server";



      listviewMessages.View = View.Details;
      listviewMessages.GridLines = true;
      listviewMessages.FullRowSelect = true;
      listviewMessages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      listviewMessages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

      //Add column header
      listviewMessages.Columns.Add("IP Address", 141);
      listviewMessages.Columns.Add("Table Name", 142);
      listviewMessages.Columns.Add("ID", 142);
      listviewMessages.Columns.Add("Clock", 142);

    }
    public void fillMessageListView(string a, string b, string c, string d)
    {
      try
      {
        if (listviewMessages.Items.Count > 500)
        {
          listviewMessages.Items.Clear();
        }
        string[] arr = { a, b, c, d };
        ListViewItem itm = new ListViewItem(arr);
        this.listviewMessages.Items.Add(itm);
      }
      catch (Exception e)
      {
        LogIslemleri.LogYaz("raised a exception while filling into listview.");
      }
    }
    private void serverThreadFunction()
    {
      try
      {
        server = new Server(this.serverType, this);
        server.startAndAcceptClients();
      }
      catch (Exception ex)
      {

      }
    }
    public void updateConnectedClientBox()
    {
      if (Server.getConnectedClientList() != null)
      {
        connectedClientsBox.Items.Clear();
        foreach (string ip in Server.getConnectedClientList())
          connectedClientsBox.Items.Add(ip);
      }
    }
  }
}
