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
        private Logger<KWTRD001> logger = new Logger<KWTRD001>();
        public string Receive(string message)
        {
            logger.Info(message);
            String callbackID = message.Split(Separator.FIELD)[0];
            String orderMessage = message.Split(Separator.FIELD)[1];
            return KiwoomApiCaller.Order(callbackID, orderMessage);
        }
    }
}
