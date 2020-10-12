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
            Application.Run(new KiwoomApiForm());
            
        }

        static void InitControllers()
        {

            //ApiSocketServer apiSocketServer = new ApiSocketServer();
            //apiSocketServer.StartServer();

            //ApiSocketClient apiSocketClient = new ApiSocketClient();
            //apiSocketClient.StartClient();
            //Thread thread = new Thread(test);
            //thread.Start();
            ApiSocketClient.Instance.ConnectServer();
            new EndProgramThread().Start();
        }
        static void InitForm()
        {
       
        }
        public static void test()
        {
            //Thread.Sleep(15000);
            //OpenApiController openApi = OpenApiController.Instance;
            //openApi.GetOPT10080("005930", "1", "0");
        }
    }
}
