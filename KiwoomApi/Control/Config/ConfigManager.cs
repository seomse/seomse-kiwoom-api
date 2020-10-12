using System;
using System.Configuration;

namespace KiwoomApi.Control.Config
{
    public class ConfigManager
    {
        private static readonly string SOCKET_LISTEN_IP = "application.socket.listen.ip";
        private static readonly string SOCKET_LISTEN_PORT = "application.socket.listen.port";

        private static readonly string SOCKET_MASTER_SERVER_IP = "application.socket.master.server.ip";
        private static readonly string SOCKET_MASTER_SERVER_RCV_PORT = "application.socket.master.server.receive.port";
        private static readonly string SOCKET_MASTER_SERVER_SND_PORT = "application.socket.master.server.send.port";

        private static readonly string KIWOOM_API_ID = "api.kiwoom.conn.id";
        private static readonly string KIWOOM_API_PACKAGE = "api.kiwoom.server.message.package.name";

        public static int ListenPort { get { return int.Parse(ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_PORT)); } }
        public static string ListenIP { get { return ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_IP); } }

        public static int MasterServerRcvPort { get { return int.Parse(ConfigurationManager.AppSettings.Get(SOCKET_MASTER_SERVER_RCV_PORT)); } }
        public static int MasterServerSndPort { get { return int.Parse(ConfigurationManager.AppSettings.Get(SOCKET_MASTER_SERVER_SND_PORT)); } }
        public static string MasterServerIP { get { return ConfigurationManager.AppSettings.Get(SOCKET_MASTER_SERVER_IP); } }

        public static string KiwoomApiID { get { return ConfigurationManager.AppSettings.Get(KIWOOM_API_ID); } }

        public static string KiwoomApiPackage { get { return ConfigurationManager.AppSettings.Get(KIWOOM_API_PACKAGE); } }

    }
}
