using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiwoomApi.Control;
using KiwoomApi.Control.Api;
using KiwoomApi.Control.Program;
using KiwoomApi.Control.Socket;
using KiwoomApi.View;
using KiwoomApi.Control.Api.KiwoomApi;

namespace KiwoomApi
{
    class Program
    {
        private static readonly Logger<Program> logger = new Logger<Program>();
        [STAThread]
        static void Main(string[] args)  
        {
            InitControllers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new KiwoomApiForm());            
        }

        static void InitControllers()
        {
            Thread t = new Thread(startConnect);
            t.Start();
        }
        private static void startConnect()
        {
            Thread.Sleep(5000);
            logger.Info("START CONNECT TO SERVER");
            ApiSocketServer apiSocketServer = new ApiSocketServer();
            apiSocketServer.StartServer();
            //new EndProgramThread().Start();
            KiwoomApiController kiwoomApiController = KiwoomApiController.Instance;
            kiwoomApiController.Init();
            ApiSocketClient.Instance.ConnectServer();

        }
    }
}
