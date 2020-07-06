using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiwoomApi.Control;
using KiwoomApi.Control.Program;
using KiwoomApi.Control.Socket;

namespace KiwoomApi
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)  
        {
            InitControllers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OpenApiForm());
        }

        static void InitControllers()
        {

            //ApiSocketServer apiSocketServer = new ApiSocketServer();
            //apiSocketServer.StartServer();

            ApiSocketClient apiSocketClient = new ApiSocketClient();
            apiSocketClient.StartClient();

            new EndProgramThread().Start();
        }
    }
}
