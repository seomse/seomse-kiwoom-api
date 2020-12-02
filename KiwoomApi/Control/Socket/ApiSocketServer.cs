using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KiwoomApi.Control;
using KiwoomApi.Control.Config;

namespace KiwoomApi.Control.Socket
{
    class ApiSocketServer
    {
        Logger<ApiSocketServer> logger = new Logger<ApiSocketServer>();
        public ApiSocketServer() {                                                                                                                     
            PORT = ConfigManager.ListenPort;
            IP = ConfigManager.ListenIP;
        }

        public void StartServer()
        {
            Thread socketServerThread = new Thread(initServer);
            socketServerThread.Start();
        }

        private TcpListener Listener = null;
        private int PORT;
        private string IP;

        private void initServer()
        {
            logger.Info("ApiSocketServer started");
            try
            {
                IPAddress address = IPAddress.Parse(IP);
                Listener = new TcpListener(address, PORT);
                Listener.Start();
                while (true)
                {
                    AcceptClient();
                }
            }
            catch (Exception e)
            {
                logger.Err(e.StackTrace);
            }
        }

        private void AcceptClient()
        {
            TcpClient client = Listener.AcceptTcpClient();
            ApiSocketReciever r = new ApiSocketReciever();
            r.StartClient(client);
        }

    }
}
