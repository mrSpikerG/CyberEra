using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Server_wpf.ViewModel {
    internal class PasswordController {




        public string GenerateNewPassword(string name,DateTime startTime, DateTime expirationTime) {

            string password = CreatePassword();
            //
            //  sql code
            //

            return password;
        }

        private string CreatePassword() {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#%&*+-=";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 16; i++) {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            string password = res.ToString();
            LoggerController.Debug($"Create password {password}");
            return password;
        }
    }
}
