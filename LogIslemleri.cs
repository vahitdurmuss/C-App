using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBSOCKETBILDIRIM_U2
{
  class LogIslemleri
  {
    public static void LogYaz(string message)
    {
      string path = AppDomain.CurrentDomain.BaseDirectory + "//logs";

      bool exists = System.IO.Directory.Exists(path);

      if (!exists)
        System.IO.Directory.CreateDirectory(path);


      using (StreamWriter sw = File.AppendText(path + "//" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt"))
      {
        sw.WriteLine(DateTime.Now + ": " + message);
      }
    }
  }
}
