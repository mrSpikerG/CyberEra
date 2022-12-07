using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberEra_Client.Control {
    internal class PasswordControl {

        private static string Name = "svchostmain";
        private static RegistryKey Regkey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\OneSettings\");
        public void CheckOldPassword(string password) {

            if (!HasOldPassword(password)) {
                AddOldPassword(password);
               // ChangePassword(password);
            } else {
                if (GetPassword() != password) {
                 //   ChangePassword(password);
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
                Regkey.Close();
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
                Regkey.Close();
                return value;
            } catch (Exception e) {
                LoggerControl.Error("Password can't be reached");
                LoggerControl.Error(e.Message);
                return "";
            }
        }

        private bool HasOldPassword(string password) {
            try {

                if (Regkey.GetValue(Name).Equals(password)) {
                    LoggerControl.Debug("Check old password cache has found password");
                    return true;
                }
                Regkey.Close();
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

    }
}
