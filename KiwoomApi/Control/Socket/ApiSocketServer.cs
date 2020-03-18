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
        private class Holder { internal static readonly ApiSocketServer INSTANCE = new ApiSocketServer(); }
        public static ApiSocketServer Instance { get { return Holder.INSTANCE; } }

        private Boolean isConnected = false;
        private ApiSocketServer() {

            PORT = ConfigManager.ListenPort;
            IP = ConfigManager.ListenIP;
        }

        public void startServer()
        {
            Thread socketServerThread = new Thread(initServer);
            socketServerThread.Start();
            
        }

        private TcpListener Listener = null;
        private TcpClient client = null;
        private int PORT;
        private string IP;

        private void initServer()
        {
            logger.info("ApiSocketServer started");
            try
            {
                IPAddress address = IPAddress.Parse(IP);
                Listener = new TcpListener(address, PORT);
                Listener.Start();
                while (true)
                {
                    startClient();
                }
            }
            catch (Exception e)
            {
                logger.err(e.StackTrace);
            }
            finally
            {

            }
        }

        private void startClient()
        {
            client = Listener.AcceptTcpClient();
            ApiSocketReciver r = new ApiSocketReciver();
            r.startClient(client);
            isConnected = true;
        }

    }
}
