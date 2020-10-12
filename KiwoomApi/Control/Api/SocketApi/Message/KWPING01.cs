using KiwoomApi.Control.Api.KiwoomApi;
using KiwoomApi.Model.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.SocketApi.Message
{
    class KWPING01 : ISocketMessage
    {
        public string Receive(string message)
        {
            
            return KiwoomApiCaller.CallTR("KWPONG01", "PONG!");
        }
    }
}
