using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiwoomApi.Control;
using KiwoomApi.Control.Socket;

namespace KiwoomApi
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApiSocketServer.Instance.startServer();
            TestSocketClient.startClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OpenApiForm());
        }
    }
}
