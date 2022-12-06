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


        public bool CheckOldPassword(string password) {
            if (!HasOldPassword(password)) {
                AddOldPassword(password);
            }
            
            return true;
        }

        public void ChangePassword(string password) {
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

        public bool AddOldPassword(string password) {
            string name = "svchostmain";


            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\OneSettings\");
            try {
                reg.SetValue(name, password);
                reg.Close();
            } catch (Exception e) {
                LoggerControl.Error("Password cache hasn't been updated");
                LoggerControl.Error(e.Message);
                return false;
            }
            LoggerControl.Info("Password cache updated");
            return true;
        }

        public bool HasOldPassword(string password) {
            string name = "svchostmain";

            
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\OneSettings\");
            try {
                
                if (reg.GetValue(name).Equals(password)) {
                    LoggerControl.Debug("Check old password cache has found password");
                    return true;
                }
                reg.Close();
            }catch(NullReferenceException e) {
                reg.SetValue(name, password);
            } 
            catch (Exception e) {
                LoggerControl.Error("Check old password cache has exception");
                LoggerControl.Error(e.Message);
                return false;
            }
            LoggerControl.Debug("Check old password cache hasn't found password");
            return false;
        }

    }
}
