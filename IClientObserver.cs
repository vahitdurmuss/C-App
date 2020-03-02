using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBSOCKETBILDIRIM_U2
{
  interface IClientObserver
  {
     void writeToEndPoint(byte[] resp);
     void subscribe();
     void unSubscribe();
     string getRemoteEndPoint();
  }
}
