using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberEra_Client.Control {
    internal class PasswordControl {

        private const int WTS_CURRENT_SESSION = -1;
        private IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        private static string Name = "svchostmain";
        private static RegistryKey Regkey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\OneSettings");

        public void CheckOldPassword(string password) {

            if (!HasOldPassword(password)) {
                AddOldPassword(password);
                //ChangePassword(password);
                //if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false)) {
                //    throw new Win32Exception();
                //}
            } else {
                if (GetPassword() != password) {
                    AddOldPassword(password);
                    //ChangePassword(password);

                    //if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false)) {
                    //    throw new Win32Exception();
                    //}
                }
            }
        }



        private void ChangePassword(string password) {
            try {
                DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                DirectoryEntry grp;
                grp = AD.Children.Find(SystemInformation.UserName, "user");
                if (grp != null) {
                    grp.Invoke("SetPassword", new object[] { password });
                }

                grp.CommitChanges();
            } catch (Exception e) {
                LoggerControl.Error("Password isn't changed");
                LoggerControl.Error(e.Message);
            }
        }

        private bool AddOldPassword(string password) {

            try {
                Regkey.SetValue(Name, password);
            } catch (Exception e) {
                LoggerControl.Error("Password cache hasn't been updated");
                LoggerControl.Error(e.Message);

                return false;
            }
            LoggerControl.Info("Password cache updated");
            return true;
        }

        private string GetPassword() {
            try {
                string value = Regkey.GetValue(Name).ToString();
                LoggerControl.Info($"Get password {value}");
                return value;
            } catch (Exception e) {
                LoggerControl.Error("Password can't be reached");
                LoggerControl.Error(e.Message);
                return "";
            }
        }

        private bool HasOldPassword(string password) {
            try {
                if (GetPassword() != "") {
                    LoggerControl.Debug("Check old password cache has found password");
                    return true;
                }
            } catch (NullReferenceException e) {
                Regkey.SetValue(Name, password);
            } catch (Exception e) {
                LoggerControl.Error("Check old password cache has exception");
                LoggerControl.Error(e.Message);
                return false;
            }
            LoggerControl.Debug("Check old password cache hasn't found password");
            return false;
        }

        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);


    }
}
