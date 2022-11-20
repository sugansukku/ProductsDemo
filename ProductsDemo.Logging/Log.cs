using NLog;
using ProductsDemo.Logging.Interface;

namespace ProductsDemo.Logging
{
    public class Log : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public Log()
        {
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}