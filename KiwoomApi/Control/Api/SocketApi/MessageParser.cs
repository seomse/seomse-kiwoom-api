using System;

namespace KiwoomApi.Control.Api.SocketApi
{
    class MessageParser
    {
        private OpenApiController openApiController = null;
        private static readonly Logger<MessageParser> logger = new Logger<MessageParser>();
        private static readonly char DEFAULT_SEPARATOR = '|';
        private static readonly String PACKAGE_NAME = "KiwoomApi.Control.Api.SocketApi.Message";
        public MessageParser()
        {
            openApiController = OpenApiController.Instance;
        }
        public string Parse(string message)
        {

            logger.Debug(message);
            try
            {
                string[] messageItem = message.Split(DEFAULT_SEPARATOR);
                string apiCode = messageItem[0];
                string apiMessage = message.Substring(apiCode.Length + 1);
                string resultMessage;

                Type t = Type.GetType(PACKAGE_NAME + "." + apiCode);
                ISocketMessage socketMessage = (ISocketMessage)(Activator.CreateInstance(t));
                resultMessage =  socketMessage.Receive(apiMessage);
                return resultMessage;
            }
            catch (Exception e)
            {
                logger.Err(e.StackTrace);
                return null;
            }
        }

    }
}
