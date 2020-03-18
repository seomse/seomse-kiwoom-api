using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Socket.Message
{
    interface IApiMessage
    {
        String toString();
        String getMessage();
    }
}
