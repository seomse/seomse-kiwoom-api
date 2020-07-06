using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.SocketApi
{
    interface ISocketMessage
    {
        string Receive(string message);
    }
}
