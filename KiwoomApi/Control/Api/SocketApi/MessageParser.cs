using KiwoomApi.Model.Def;
using System;

namespace KiwoomApi.Control.Api.SocketApi
{
    class MessageParser
    {
        private static readonly Logger<MessageParser> logger = new Logger<MessageParser>();
        private static readonly string PACKAGE_NAME = "KiwoomApi.Control.Api.SocketApi.Message";
        public static string Parse(string message)
        {
            logger.Debug(message);
            try
            {
                string[] messageItem = message.Split(Separator.FIELD);
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
