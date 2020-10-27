using KiwoomApi.Control.Api.SocketApi;
using KiwoomApi.Control.Config;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Socket
{
    class ApiSocketClient
    {
        private static readonly Logger<ApiSocketClient> logger = new Logger<ApiSocketClient>();

        private static int serverRcvPort = 33333;
        private static int serverSndPort = 33334;
        private static string serverIP = "127.0.0.1";

        /* Singleton */
        private static readonly Lazy<ApiSocketClient> lazy = new Lazy<ApiSocketClient>(() => new ApiSocketClient());
        public static ApiSocketClient Instance => lazy.Value;

        private string apiId = string.Empty;
        private string serverApiPackage = string.Empty;
        private int CONNECT_WAIT_TIME = 3000;
        private ApiSocketClient() {
            
            serverIP = ConfigManager.MasterServerIP;
            
            serverRcvPort = ConfigManager.MasterServerRcvPort;
            serverSndPort = ConfigManager.MasterServerSndPort;
            while (true)
            {
                try
                {
                    rcvClient = new TcpClient(serverIP, serverRcvPort);
                    sndClient = new TcpClient(serverIP, serverSndPort);
                    break;
                } catch
                {
                    Thread.Sleep(CONNECT_WAIT_TIME);
                    logger.Err("SERVER ([RCV]" + serverIP + ":" + serverRcvPort +",[SND]"+ serverIP + ":" + serverSndPort
                        + ") CONNECT FAIL. WAIT [" +(CONNECT_WAIT_TIME / 1000 )+"] SECONDS..");
                    rcvClient = null;
                    sndClient = null;
                    continue;
                }
            }
            apiId = ConfigManager.KiwoomApiID;
            serverApiPackage = ConfigManager.KiwoomApiPackage;

        }
        /* Singleton */

        private static readonly char START = (char)0;
        private static readonly char END   = (char)1;

        private static TcpClient rcvClient = null;
        private static TcpClient sndClient = null;

        public void ReceiveThreadStart()
        {
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
        }
        
        private void ReceiveMessage()
        {
            NetworkStream ns = rcvClient.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes,0, receivedBytes.Length-1)) > 0)
            {
                string message = GetApiReceiveStr(receivedBytes);
                MessageParser.Parse(message);
            }
        }

        Boolean isConnected = false;
        public Boolean ConnectServer()
        {
            if (isConnected == false)
            {
                logger.Info(String.Format("server connected. {0}:{1},{2}", serverIP, serverSndPort,serverRcvPort));
                SendMessage("KWCONN01", apiId);
                ReceiveThreadStart();
                isConnected = true;
            }
            return isConnected;
        }

        public void SendMessage(String apiCode, string message)
        {
            try
            {
                NetworkStream ns = sndClient.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                StreamWriter sw = new StreamWriter(ns, Encoding.UTF8); // Send message
                string sendMessage = string.Empty;
                if (serverApiPackage == null)
                {
                    sendMessage = "D" + apiCode + "," + message;
                }
                else
                {
                    sendMessage = "C" + serverApiPackage + "," + apiCode + "," + message;
                }

                char[] buff = GetApiChars(sendMessage);
                sw.Write(buff);// 메시지 보내기
                sw.Flush();
            }

            catch (Exception e)
            {
                logger.Err(e.Message);
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                NetworkStream ns = rcvClient.GetStream(); // 소켓에서 메시지를 가져오는 스트림
                StreamWriter sw = new StreamWriter(ns, Encoding.UTF8); // Send message
                string sendMessage = message;
                char[] buff = GetApiChars(sendMessage);
                sw.Write(buff);// 메시지 보내기
                sw.Flush();
            }

            catch (Exception e)
            {
                logger.Err(e.Message);
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

        public static string GetApiReceiveStr(byte[] messageBytes)
        {
            
            int copyCnt = 0;
            for (int i = 0; i < messageBytes.Length; i++)
            {
                char checkChar = (char)messageBytes[i];
                if (checkChar == START)
                {
                    continue;
                }
                else if (checkChar == END)
                {
                    break;
                }
                copyCnt++;
            }
            byte[] buff = new byte[copyCnt];
            copyCnt = 0;
            for (int i=0; i < messageBytes.Length; i++)
            {
                char checkChar = (char)messageBytes[i];
                if (checkChar == START)
                {
                    continue;
                } else if(checkChar == END)
                {
                    break;
                }
                buff[copyCnt++] = messageBytes[i];
            }
            return Encoding.UTF8.GetString(buff).Substring(1);
        }
    }
}
