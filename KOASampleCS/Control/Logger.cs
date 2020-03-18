using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control
{
    class Logger
    {
        public static void info(String message)
        {
            Console.WriteLine(message);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
