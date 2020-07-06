using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiwoomApi.Control.Api.SocketApi;


namespace KiwoomApi.Control.Api.SocketApi.Message
{
    class MINU0001 : DefaultSocketMessage, ISocketMessage
    {
        public string Receive(string message)
        {
            if (IsReady)
                openApi.GetOPT10006(message);
            return "";
        }
    }
}
