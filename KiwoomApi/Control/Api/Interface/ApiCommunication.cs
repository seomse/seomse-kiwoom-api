using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Api.Interface
{
    class ApiCommunication
    {
        OpenApiController openApiController;

        private static readonly char DEFAULT_SEPARATOR = '|';

        public ApiCommunication()
        {
            openApiController = OpenApiController.Instance;
        }
        public string parseMessage(string message)
        {
            string[] messageItem = message.Split(DEFAULT_SEPARATOR);
            string apiCode = messageItem[0];
            string apiMessage = message.Substring(apiCode.Length + 1);
            string resultMessage;
            switch (apiCode) {
                default:
                    resultMessage = ""; break;
            }
            return resultMessage;
        }

    }
}
