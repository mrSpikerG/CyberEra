using log4net;


namespace CyberEra_Server_wpf.ViewModel {
    internal class LoggerController {

        
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message) { log.Info(message); }

        public static void Warn(string message) { log.Warn(message); }

        public static void Error(string message) { log.Error(message); }

        public static void Fatal(string message) { log.Fatal(message); }

        public static void Debug(string message) { log.Debug(message); }



    }
}
