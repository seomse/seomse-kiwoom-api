using KiwoomApi.Control.Api.KiwoomApi;
using KiwoomApi.Model.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.SocketApi.Message
{
    class KWTRD001 : ISocketMessage
    {
        public string Receive(string message)
        {
            return KiwoomApiCaller.Order(message);
        }
    }
}
