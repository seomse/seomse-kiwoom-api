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

        public static int ListenPort { get { return int.Parse( ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_PORT) ); } }
        public static string ListenIP { get { return ConfigurationManager.AppSettings.Get(SOCKET_LISTEN_IP); } }
    }
}
