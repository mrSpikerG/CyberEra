using NLog;
using System.Windows.Forms;

namespace CyberEra_Server.Control
{
    internal class ErrorController
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public static void ShowError(string message)
        {
            Log.Error(message);
            MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowError(string message, string stacktrace) {
            Log.Error(message);
            Log.Error(stacktrace);
            MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
