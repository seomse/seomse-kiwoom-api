using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.SocketApi
{
    abstract class DefaultSocketMessage 
    {
        protected OpenApiController openApi = OpenApiController.Instance;
        protected Boolean IsReady { get { return openApi.IsReady; } }
    }
}
