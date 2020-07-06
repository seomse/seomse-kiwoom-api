using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiwoomApi.Control.Program
{
    internal class EndProgramThread 
    {
        private static readonly Logger<EndProgramThread> logger = new Logger<EndProgramThread>();
        private Thread endProgramThread=null;
        public EndProgramThread()
        {
            endProgramThread = new Thread(ProgramEndCheck);
        }
        
        public void Start()
        {

            endProgramThread.Start();
        }

        private void ProgramEndCheck()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Boolean isDestroy = false;
            while (true)
            {
                int hour = DateTime.Now.Hour;
                TimeSpan ts = sw.Elapsed;
                // 오전 8시 이후이고 프로그램 실행시간이 1시간 보다 크면 종료
                // 다른 프로그램으로 스케쥴링 하여 프로그램을 자동 실행하게 하면 매번 업데이트를 실행 함.
                if (hour == 8 && ts.TotalHours > 1.0)
                {
                    // 종료가 안될시 강제 종료
                    if (isDestroy)
                    {
                        System.Diagnostics.Process[] mProcess = System.Diagnostics.Process.GetProcessesByName("opstarter");
                        foreach (System.Diagnostics.Process p in mProcess)
                        {
                            p.Kill();
                        }
                        logger.Info(Application.ProductName);

                        Thread.Sleep(1000);
                        mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
                        foreach (System.Diagnostics.Process p in mProcess)
                        {
                            p.Kill();
                        }
                    }
                    logger.Info("PROGRAM DESTROY TIME!!" + hour);
                    Thread.Sleep(10000);
                    isDestroy = true;
                    Application.Exit();
                }
                Thread.Sleep(3000);
            }
        }
    }
}
