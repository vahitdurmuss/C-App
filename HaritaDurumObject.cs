using DATA_WORK_NAMESPACE_U2;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBSOCKETBILDIRIM_U2
{
  class HaritaDurumObject:IHaritaDurum
  {
    private static HashSet<IClientObserver> observers;
    private static DatabaseFactory db;
    private int servertype;
    private ServerForm webSocketUI;
    private static string returnId;

    public HaritaDurumObject(int servertype,ServerForm form)
    {
      this.servertype = servertype;
      this.webSocketUI = form;
      observers = new HashSet<IClientObserver>();
      db = new DatabaseFactory(servertype);

    }
    public HaritaDurumObject(int servertype)
    {
      this.servertype = servertype;
      observers = new HashSet<IClientObserver>();
      db = new DatabaseFactory(servertype);
    }
    private  void OnChangeNotifyMap(object sender, OracleNotificationEventArgs eventArgs)
    {
      try
      {
        Console.WriteLine("çağrıldı");
        string rowid = "";
        foreach (System.Data.DataRow item in eventArgs.Details.Rows)
        {
          rowid = item.ItemArray[2].ToString();
          break;
        }

        string sql = "SELECT A.ID FROM TABLE_NAME A WHERE A.ROWID LIKE '%" + rowid + "%'";

        returnId = db.ExecuteScalar(sql).ToString();
        if (!String.IsNullOrEmpty(returnId))
        {
          byte[] response = Encoding.ASCII.GetBytes("  TABLE_NAME:" + returnId);
          response[0] = 0x81; // denotes this is the final message and it is in text
          response[1] = (byte)(response.Length - 2); // payload size = message - header size
          notifyObservers(response);
        }
      }
      catch (Exception e)
      {
      }
    }
    private  void OnChangeNotifyError(object sender, OracleNotificationEventArgs eventArgs)
    {
      try
      {
      
        string rowid = "";
        foreach (System.Data.DataRow item in eventArgs.Details.Rows)
        {
          rowid = item.ItemArray[2].ToString();
          break;
        }

        string sql = "SELECT A.ID FROM TABLE_NAME A WHERE A.ROWID LIKE '%" + rowid + "%'";

        returnId = db.ExecuteScalar(sql).ToString();

        if (!String.IsNullOrEmpty(returnId))
        {
          byte[] response = Encoding.ASCII.GetBytes("  TABLE_NAME_HATA:" + returnId);
          response[0] = 0x81; // denotes this is the final message and it is in text
          response[1] = (byte)(response.Length - 2); // payload size = message - header size
          notifyObservers(response);          
        }
      }
      catch (Exception e)
      {

      }
    }
    public void notifyObservers(byte[] response)
    {
      foreach (IClientObserver observer in observers)
      {
        try
        {
          observer.writeToEndPoint(response);
          string[] arr = { observer.getRemoteEndPoint(), "TABLE_NAME HATA", returnId, DateTime.Now.ToString() };
          webSocketUI.Invoke(new myDelegate(webSocketUI.fillMessageListView), new Object[] { arr[0], arr[1], arr[2], arr[3]});
        }
        catch (Exception e)
        {
          string x = e.Message;
        }
      }
    }
    public delegate void myDelegate(string a,string b,string c,string d);
    public void checkDBDependency()
    {
     bool isAvaible=db.isAvaibleDependency();
     if(isAvaible==false)
     {
       listenDataBase();
     }

    }
    public void listenDataBase()
    {
      db.StartDBListening("select * from TABLE_NAME", OnChangeNotifyMap);
      db.StartDBListening("select * from TABLE_NAME", OnChangeNotifyError);
    }
    public void subscribeObserver(IClientObserver observer)
    {
      lock (this)
      {
        observers.Add(observer);
      }
    }
    public void unSubscribeObserver(IClientObserver observer)
    {
      lock (this)
      {
        observers.Remove(observer);
      }
    }
    public List<string> getRemoteEndPointAllObservers()
    {
      List<string> observerList = new List<string>();

      foreach (IClientObserver observer in observers)
      {
        observerList.Add(observer.getRemoteEndPoint());
      }
      return observerList;
    }

  }
}
