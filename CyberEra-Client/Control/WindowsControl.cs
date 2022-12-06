using Microsoft.Win32;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace CyberEra_Client.Control {
    internal class WindowsControl {

        public bool SetAutorunValue(bool autorun) {
            string name = "svchostmain";
            string ExePath = Application.ExecutablePath;

            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try {
                if (autorun) {
                    reg.SetValue(name, ExePath);

                } else {
                    reg.DeleteValue(name);
                }
                reg.Close();
            } catch {
                LoggerControl.Error("Autorun wasn't setuped");
                return false;
            }
                LoggerControl.Error("Autorun setuped");
            return true;
        }

        


        //
        //  Запретить закрытие
        //
        public void Client_FormClosing_deny(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing |
                e.CloseReason == CloseReason.MdiFormClosing |
                e.CloseReason == CloseReason.TaskManagerClosing |

                e.CloseReason == CloseReason.FormOwnerClosing) {
                e.Cancel = true;
            }
        }


        //
        //  Разрешить закрытие
        //
        public void Client_FormClosing_access(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing |
                e.CloseReason == CloseReason.MdiFormClosing |
                e.CloseReason == CloseReason.TaskManagerClosing |


                e.CloseReason == CloseReason.FormOwnerClosing) {
                e.Cancel = false;
            }
        }
    }
}
