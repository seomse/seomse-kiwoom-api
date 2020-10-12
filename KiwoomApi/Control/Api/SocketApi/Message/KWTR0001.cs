using KiwoomApi.Control.Api.KiwoomApi;
using KiwoomApi.Model.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.SocketApi.Message
{
    class KWTR0001 : ISocketMessage
    {
        public string Receive(string message)
        {
            String trCode = message.Split(Separator.FIELD)[0];
            String trMessage = message.Split(Separator.FIELD)[1];
            return KiwoomApiCaller.CallTR(trCode, trMessage);
        }
    }
}
