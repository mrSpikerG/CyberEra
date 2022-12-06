using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Client.Control {
    internal class LoggerControl {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message) { log.Info(message); }

        public static void Warn(string message) { log.Warn(message); }

        public static void Error(string message) { log.Error(message); }

        public static void Fatal(string message) { log.Fatal(message); }

        public static void Debug(string message) { log.Debug(message); }
        

    }
}
