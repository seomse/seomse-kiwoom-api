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
        private Logger<KWTR0001> logger = new Logger<KWTR0001>();
        public string Receive(string message)
        {
            logger.Info(message);
            String trCode = message.Split(Separator.FIELD)[0];
            String callbackID = message.Split(Separator.FIELD)[1];
            String trMessage = message.Split(Separator.FIELD)[2];
            
            return KiwoomApiCaller.CallTR(trCode, callbackID , trMessage);
        }
    }
}
