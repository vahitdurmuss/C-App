using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WEBSOCKETBILDIRIM_U2;


namespace DATA_WORK_NAMESPACE_U2
{
  public class DatabaseFactory
  {
    // NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private enum ConnectionStrings
    {
      live = 1,
      test = 2
    }

    private string ConnectionString { get; set; }
    private OracleConnection _Connection { get; set; }
    private static OracleDependency dep;
    public DatabaseFactory()
    {

#if DEBUG
      ConnectionString = "Data Source=xxxx; User Id=yyy; Password=zzz";
#else
      ConnectionString = "Data Source=xxxx; User Id=yyy; Password=zzz";
#endif

      if (_Connection == null)
        _Connection = new OracleConnection(ConnectionString);
    }
    public DatabaseFactory(int dbCode)
    {
      if (dep == null)
        dep = new OracleDependency();
      switchDBConnection(dbCode);
    }

    /// <summary>
    /// Veritabanı bağlantısını açar
    /// </summary>
    /// <returns></returns>
    private bool ConnectionOpen()
    {
      bool result = false;
      if (_Connection != null)
      {
        if (_Connection.State != ConnectionState.Open)
        {
          _Connection.Open();
          result = true;
        }
        else if (_Connection.State == ConnectionState.Open)
        {
          result = true;
        }
      }
      else
      {
        result = false;

        //   LogIslemleri.LogYaz("Veritabanı bağlantısı yapılmamış!");
      }
      return result;
    }
    /// <summary>
    /// Veritabanı bağlantısının açık olmadığını kontrol eder
    /// </summary>
    /// <returns></returns>
    private bool IsConnectionOpen()
    {
      if (_Connection != null)
      {
        if (_Connection.State == ConnectionState.Open)
          return true;
        else
          return false;
      }
      else
      {
        return false;
        // LogIslemleri.LogYaz("Veritabanı bağlantısı yapılmamış!");
      }
    }
    /// <summary>
    /// Veritabanı bağlantısını kapatır
    /// </summary>
    /// <returns></returns>
    private bool CloseConnection()
    {
      if (_Connection != null)
      {
        if (_Connection.State != ConnectionState.Closed)
          _Connection.Close();
        return true;
      }
      else
      {
        //  LogIslemleri.LogYaz("Veritabanı bağlantısı yapılmamış!");
        return false;
      }
    }
    private void initConnection(string pconnectionString)
    {
      CloseConnection();
      _Connection = new OracleConnection(pconnectionString);
    }
    public int switchDBConnection(int serverCode)
    {
      int codeAfterSwitch = 0;
      switch (serverCode)
      {
        case (int)ConnectionStrings.live:
          initConnection("Data Source=xxx; User Id=yyy; Password=zzz");
          codeAfterSwitch = 1;
          break;
        case (int)ConnectionStrings.test:
          initConnection("Data Source=xxx; User Id=yyy; Password=zzz");
          codeAfterSwitch = 2;
          break;
        default:
          initConnection("Data Source=xxx; User Id=yyy; Password=zzz");
          break;
      }

      return codeAfterSwitch;
    }
    public int StartDBListening(string query, OnChangeEventHandler func)
    {
      int result = -1;
      OracleCommand cmd = null;
      try
      {
        if (ConnectionOpen())
        {
          cmd = new OracleCommand(query, _Connection);
          cmd.AddRowid = true;

          //OracleDependency dep = new OracleDependency(cmd);
          dep.AddCommandDependency(cmd);

          cmd.Notification.IsNotifiedOnce = false;

          bool buldu = false;
          foreach (var item in dep.RegisteredResources)
          {
            if (item.ToString() == query)
            {
              buldu = true;
            }
          }



          if (buldu == false)
            dep.OnChange += new OnChangeEventHandler(func);

          cmd.ExecuteNonQuery();
        }
      }
      catch (OracleException oex)
      {

      }
      catch (Exception ex)
      {
        LogIslemleri.LogYaz(ex.Message);
        result = -1;
      }
      finally
      {
        if (cmd != null)
          cmd.Dispose();
        CloseConnection();
      }

      return result;
    }
    public int StopDBListening()
    {
      try
      {
        if (ConnectionOpen())
        {
          if (dep.IsEnabled)
            dep.RemoveRegistration(_Connection);
        }
      }
      catch (Exception ex)
      {
        return -1;
      }
      return 1;
    }
    public bool isAvaibleDependency()
    {
      int i = 0;
      if (dep != null)
      {
        if (!dep.IsEnabled)
        {
          return false;
        }
        else
        {
          foreach (var item in dep.RegisteredResources)
          {
            i++;
          }
          if (i != 2)
          {
            dep.RemoveRegistration(_Connection);
            return false;
          }
          return true;

        }

      }
      else
        return false;


    }
    public class ExceptionLog
    {
      public string ExceptionMessage { get; set; }
      public string CallerName { get; set; }
      public string CallerFilePath { get; set; }
      public int CallerLineNumber { get; set; }
      public string ProgramUserId { get; set; }
      public DateTime ExceptionDateTime { get; set; }
    }
    public object ExecuteScalar(string query)
    {
      object result = null;
      OracleCommand cmd = null;
      try
      {
        if (ConnectionOpen())
        {
          cmd = _Connection.CreateCommand();
          cmd.CommandText = query;
          result = cmd.ExecuteScalar();
          if (result == null)
            result = "";
        }
      }
      catch (Exception ex)
      {
        LogIslemleri.LogYaz(ex.Message);
        result = "";
      }
      finally
      {
        if (cmd != null)
          cmd.Dispose();
        CloseConnection();
      }
      return result;
    }
  }
}