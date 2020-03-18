using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiwoomApi.Control
{
    public class Logger<T>
    {
        private readonly Type type;
        public Logger()
        {
            Type t = typeof(T);
            this.type = t;
        }
        public void info(String message)
        {
            Console.WriteLine("[INFO]["+ type.Name+ "]"+message);
        }

        public void err(String message)
        {
            Console.WriteLine("[ERRO][" + type.Name + "]" + message);
        }
    }
}
