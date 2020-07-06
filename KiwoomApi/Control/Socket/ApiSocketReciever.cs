using KiwoomApi.Control.Api.SocketApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Socket
{
    class ApiSocketReciever
    {
        private Logger<ApiSocketReciever> logger = new Logger<ApiSocketReciever>();
        private NetworkStream NS = null;
        private StreamReader SR = null;
        private StreamWriter SW = null;
        private MessageParser messageParser = new MessageParser();
        public void startClient(TcpClient clientSocket)
        {
            NS = clientSocket.GetStream(); 
            SR = new StreamReader(NS, Encoding.UTF8); 
            SW = new StreamWriter(NS, Encoding.UTF8); 
            string message = string.Empty;
            try
            {
                while (clientSocket.Connected == true) 
                {
                    message = SR.ReadLine();
                    string responseMessage = messageParser.Parse(message);
                    logger.Debug("responseMessage:" + responseMessage);
                    SW.WriteLine(responseMessage); 
                    SW.Flush();
                    Thread.Sleep(201);
                }
            }
            catch (Exception e)
            {
                logger.Err(e.StackTrace);
            }
            finally
            {
                SW.Close();
                SR.Close();
                clientSocket.Close();
                NS.Close();
            }

        
        }
    }
}
