using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBSOCKETBILDIRIM_U2
{
  interface IHaritaDurum
  {
     void subscribeObserver(IClientObserver observer);
     void unSubscribeObserver(IClientObserver observer);
     void notifyObservers(byte[] response);      
  }
}
