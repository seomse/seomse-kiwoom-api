using log4net;
using log4net.Appender;
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
        ILog logger = null;
        public Logger()
        {
            Type t = typeof(T);
            this.type = t;
            // 로그 매니져 세팅
            var repository = LogManager.GetRepository();
            repository.Configured = true;
            // 콘솔 로그 패턴 설정
            var consoleAppender = new ConsoleAppender();
            consoleAppender.Name = "Console";
            logger = LogManager.GetLogger(type);

        }
        public void Info(String message)
        {
            logger.Info(message);
        }

        public void Err(String message)
        {
            logger.Error(message);
        }

        public void Warn(String message)
        {
            logger.Warn(message);
        }

        public void Debug(String message)
        {
            logger.Debug(message);
        }
    }
}
