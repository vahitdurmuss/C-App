using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WEBSOCKETBILDIRIM_U2
{
  class Server
  {
    private static TcpListener _server;
    public static List<string> bannedClientIPs;
    private static HaritaDurumObject haritaDurumObject;
    private static ServerForm webSocketUI;

    public Server(int servertype, ServerForm pwebSocketUI)
    {
      switch (servertype)
      {
        case 0: // local
          _server = new TcpListener(IPAddress.Parse("127.0.0.1"), 90);
          break;
        case 1: //canli
          //_server = new TcpListener(IPAddress.Parse("10.100.2.99"), 90);
          _server = new TcpListener(IPAddress.Parse("127.0.0.1"), 90);
          break;
        case 2: //test
          _server = new TcpListener(IPAddress.Parse("127.0.0.1"), 90);
          break;
      }

      webSocketUI = pwebSocketUI;
      haritaDurumObject = new HaritaDurumObject(servertype, webSocketUI);
      haritaDurumObject.listenDataBase();
      bannedClientIPs = new List<string>();

    }
    public void startAndAcceptClients()
    {

      _server.Start();

      while (true)
      {
        TcpClient tempTcpClient = _server.AcceptTcpClient();
        haritaDurumObject.listenDataBase();
        haritaDurumObject.checkDBDependency(); //Her Client bağlandığında DB dependcy kopup olup olmadığı kontrol edilir.
        ClientObject comingClientObject = new ClientObject(tempTcpClient, haritaDurumObject);

        bool isblocked = isClientBlocked(comingClientObject);

        if (isblocked == false)
        {
          string result = bindClientToServer(comingClientObject);
          if (result.Equals("ok"))
          {

          }
        }
        else
        {
          //Client Engellenmiş Clientlar arasında.
        }

      }
    }
    private static bool isClientBlocked(ClientObject clientObject)
    {
      string ip = clientObject.getClientIPAddress();

      if (bannedClientIPs.Contains(ip)) //engellenen ip adreslerini reddetme işlemi
      {
        clientObject.block();
        return true;
      }
      else
      {
        return false;
      }
    }
    private string bindClientToServer(ClientObject clientobject)
    {
      try
      {
        clientobject._clientThread = new Thread(new ParameterizedThreadStart(WaitClientMessage));
        clientobject._clientThread.Start(clientobject);
        return "ok";
      }
      catch (Exception ex)
      {

        return ex.Message;
      }
    }
    private static void WaitClientMessage(object x)
    {
      try
      {
        ClientObject tmpClientObject = (ClientObject)x;
        NetworkStream stream = tmpClientObject.getNetworkStream();

        string ipadress = tmpClientObject.getClientIPAddress();

        if (bannedClientIPs != null && bannedClientIPs.Contains(ipadress)) //engellenen ip adreslerini reddetme işlemi
        {
          tmpClientObject.block();
          return;
        }

        int i = 0;
        //enter to an infinite cycle to be able to handle every change in stream
        while (true)
        {
          while (!stream.DataAvailable)
          {
            if (bannedClientIPs != null && bannedClientIPs.Contains(ipadress))
            {
              tmpClientObject.unSubscribe();
              tmpClientObject.block();
              return;
            }

            Thread.Sleep(10);
          }

          Byte[] bytes = new Byte[tmpClientObject.getDataAmount()];


          if (bytes.Count() == 0)
            continue;
          stream.Read(bytes, 0, bytes.Length);

          //translate bytes of request to string
          String data = Encoding.UTF8.GetString(bytes);

          if (new System.Text.RegularExpressions.Regex("^GET").IsMatch(data))
          {
            const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

            Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + eol
                + "Connection: Upgrade" + eol
                + "Upgrade: websocket" + eol
                + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                    System.Security.Cryptography.SHA1.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(
                            new System.Text.RegularExpressions.Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                        )
                    )
                ) + eol
                + eol);

            stream.Write(response, 0, response.Length);
            tmpClientObject.subscribe();
            webSocketUI.Invoke(new updateConnectedClientBoxDelegate(webSocketUI.updateConnectedClientBox));
            Thread.Sleep(10);

          }
          else
          {
            tmpClientObject.unSubscribe();
            webSocketUI.Invoke(new updateConnectedClientBoxDelegate(webSocketUI.updateConnectedClientBox));
            Thread.Sleep(10);
            break;
          }
        }

      }
      catch (ThreadAbortException tae)
      {
        LogIslemleri.LogYaz(" WaitClientMessage thread içerisinde sorun oluştu ");
      }
    }
    public delegate void updateConnectedClientBoxDelegate();
    public static List<string> getConnectedClientList()
    {
      try
      {
        return haritaDurumObject.getRemoteEndPointAllObservers();
      }
      catch
      {
        return null;
      }

    }

  }
}

