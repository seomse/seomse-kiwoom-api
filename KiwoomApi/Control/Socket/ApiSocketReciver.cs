using KiwoomApi.Control.Api.Interface;
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
    class ApiSocketReciver
    {
        private Logger<ApiSocketReciver> logger = new Logger<ApiSocketReciver>();
        private NetworkStream NS = null;
        private StreamReader SR = null;
        private StreamWriter SW = null;
        private ApiCommunication api = new ApiCommunication();
        public void startClient(TcpClient clientSocket)
        {
            NS = clientSocket.GetStream(); 
            SR = new StreamReader(NS, Encoding.UTF8); 
            SW = new StreamWriter(NS, Encoding.UTF8); 
            string GetMessage = string.Empty;
            try
            {
                while (clientSocket.Connected == true) 
                {
                    GetMessage = SR.ReadLine();
                    string apiResponseMessage = api.parseMessage(GetMessage);
                    SW.WriteLine(apiResponseMessage); 
                    SW.Flush();
                    Thread.Sleep(201);
                }
            }
            catch (Exception e)
            {
                logger.err(e.StackTrace);
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
