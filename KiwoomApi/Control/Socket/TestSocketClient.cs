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
    class TestSocketClient
    {
        static Logger<TestSocketClient> logger = new Logger<TestSocketClient>();
        public static void startClient()
        {
            Thread socketServerThread = new Thread(test);
            socketServerThread.Start();
        }

        public static void test()
        {
            int PORT = 21119;
            string IP = "localhost";

            NetworkStream NS = null;
            StreamReader SR = null;
            StreamWriter SW = null;
            TcpClient client = null;

            try
            {
                client = new TcpClient(IP, PORT); //client 연결
                logger.info(String.Format( "{0}:{1}에 접속하였습니다.", IP, PORT ));
                NS = client.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                SR = new StreamReader(NS, Encoding.UTF8); // Get message
                SW = new StreamWriter(NS, Encoding.UTF8); // Send message

                string SendMessage = string.Empty;
                string GetMessage = string.Empty;

                while ((SendMessage = Console.ReadLine()) != null)
                {
                    SW.WriteLine(SendMessage); // 메시지 보내기
                    SW.Flush();

                    GetMessage = SR.ReadLine();
                    logger.info(GetMessage);
                }
            }

            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                if (SW != null) SW.Close();
                if (SR != null) SR.Close();
                if (client != null) client.Close();
            }

        }
    }
}
