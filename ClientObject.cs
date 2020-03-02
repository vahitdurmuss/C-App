using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WEBSOCKETBILDIRIM_U2
{
  class ClientObject: IClientObserver
  {
    private  TcpClient _client;
    public Thread _clientThread;
    private IHaritaDurum _haritaDurum;
    public ClientObject(TcpClient client)
    {
      _client = client;      
    }

    public ClientObject(TcpClient client,IHaritaDurum haritaDurum)
    {
      _client = client;
      _haritaDurum = haritaDurum;
    }

    public string getClientIPAddress()
    {
      return  _client.Client.RemoteEndPoint.ToString().Split(':')[0];
    }       
    public void block()
    {
      try
      {
        _client.Close();
      }
      catch
      {

      }
     
    }
    public NetworkStream getNetworkStream()
    {
      return _client.GetStream();
    }
    public int getDataAmount()
    {
      return _client.Available;
    }
    public void subscribe()
    {
      _haritaDurum.subscribeObserver(this);
    }
    public void unSubscribe()
    {
      _haritaDurum.unSubscribeObserver(this);
    }

    public void writeToEndPoint(byte[] resp)
    {
      NetworkStream ns= getNetworkStream();
      ns.Write(resp,0,resp.Length);
    }

    public string getRemoteEndPoint()
    {
      return _client.Client.RemoteEndPoint.ToString();
    }
  }
}
