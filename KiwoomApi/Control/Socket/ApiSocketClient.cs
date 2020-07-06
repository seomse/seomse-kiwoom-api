using KiwoomApi.Control.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control
{
    class ApiSocketClient
    {
        private static readonly Logger<ApiSocketClient> logger = new Logger<ApiSocketClient>();

        private static readonly char START = (char)0;
        private static readonly char END   = (char)1;


        public void StartClient()
        {
            Thread socketServerThread = new Thread(ConnectServer);
            socketServerThread.Start();
        }

        public static void ConnectServer()
        {
            int PORT = ConfigManager.MasterServerPort;
            string IP = ConfigManager.MasterServerIP;

            NetworkStream NS = null;
            StreamReader SR = null;
            StreamWriter SW = null;
            TcpClient client = null;

            try
            {
                client = new TcpClient(IP, PORT); //client 연결

                logger.Info(String.Format( "server connected. {0}:{1}", IP, PORT ));
                NS = client.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                SR = new StreamReader(NS, Encoding.UTF8); // Get message
                SW = new StreamWriter(NS, Encoding.UTF8); // Send message

                string sendMessage = string.Empty;
                string GetMessage = string.Empty;

                string apiId = ConfigManager.KiwoomApiID;

                sendMessage = "DCONN0001," + apiId;

                char[] buff = GetApiChars(sendMessage);
                SW.Write(buff);// 메시지 보내기
                SW.Flush();

                //while (true)
                //{

                //   Thread.Sleep(1000);
                //}

                //SW.Close();

                //while ((SendMessage = Console.ReadLine()) != null)
                //{
                //SW.WriteLine(SendMessage); // 메시지 보내기
                //SW.Flush();

                //                GetMessage = SR.ReadLine();
                //logger.Info(GetMessage);
                //}
            }

            catch (Exception e)
            {
                logger.Err(e.Message);
            }
            finally
            {
                if (SW != null) SW.Close();
                if (SR != null) SR.Close();
                if (client != null) client.Close();
            }
        }

        private static char[] GetApiChars(string sendMessage)
        {
            char[] messageChars = sendMessage.ToCharArray();
            char[] buff = new char[messageChars.Length + 2];
            buff[0] = START;

            messageChars.CopyTo(buff, 1);
            buff[buff.Length - 1] = END;
            return buff;
        }
    }
}
