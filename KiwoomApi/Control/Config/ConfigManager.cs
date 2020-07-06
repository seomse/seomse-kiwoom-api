using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomApi.Control.Config
{
    public class ConfigManager
    {
        private static readonly String SOCKET_LISTEN_IP = "application.socket.listen.ip";
        private static readonly String SOCKET_LISTEN_PORT = "application.socket.listen.port";

        private static readonly String SOCKET_MASTER_SERVER_IP = "application.socket.master.server.ip";
        private static readonly String SOCKET_MASTER_SERVER_PORT = "application.socket.master.server.port";

        private static readonly String KIWOOM_API_ID = "api.kiwoom.conn.id";

        public static int ListenPort { get { return int.Parse(ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_PORT)); } }
        public static string ListenIP { get { return ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_IP); } }

        public static int MasterServerPort { get { return int.Parse(ConfigurationManager.AppSettings.Get(SOCKET_MASTER_SERVER_PORT)); } }
        public static string MasterServerIP { get { return ConfigurationManager.AppSettings.Get(SOCKET_MASTER_SERVER_IP); } }

        public static string KiwoomApiID { get { return ConfigurationManager.AppSettings.Get(KIWOOM_API_ID); } }

    }
}
