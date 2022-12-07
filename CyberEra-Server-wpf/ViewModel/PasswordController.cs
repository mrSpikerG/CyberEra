using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.Model.DataBaseModels;
using Dapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Server_wpf.ViewModel {
    internal class PasswordController {




        public static string TryGenerateNewPassword(string name, DateTime expirationTime) {

            string password = CreatePassword();

            using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {

                string sqlInsert = "INSERT INTO Passwords([UserName],[Password],[TimeExpitation]) VALUES (@Name,@Password,CAST(@Expire AS DateTime))";
                var variables = new { Name = name, Password = password, Expire = expirationTime.ToString("yyyy-MM-dd HH:mm:ss.000") };

                var exists = db.ExecuteScalar<bool>("SELECT count(1) FROM PASSWORDS WHERE UserName=@Name", new { Name = name });
                if (!exists) {

                    //  CAST('2015-12-25 15:32:06.427' AS DateTime)
                    db.Execute(sqlInsert, variables);
                    return password;
                }

                UserPassword pass = db.Query<UserPassword>("SELECT * FROM Passwords WHERE UserName=@Name", new { Name = name }).FirstOrDefault();
                if (pass.TimeExpitation > DateTime.Now) {
                    DeletePassword(name);
                    db.Execute(sqlInsert, variables);
                    return password;
                }
                return "";
            }
        }

        public static string GeneratePasswordByForce(string name, DateTime expirationTime) {
            string password = CreatePassword();

            string sqlInsert = "INSERT INTO Passwords([UserName],[Password],[TimeExpitation]) VALUES (@Name,@Password,CAST(@Expire AS DateTime))";
            var variables = new { Name = name, Password = password, Expire = expirationTime.ToString("yyyy-MM-dd HH:mm:ss.000") };


            using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                var exists = db.ExecuteScalar<bool>("SELECT count(1) FROM PASSWORDS WHERE UserName=@Name", new { Name = name });
                if (exists) {
                    DeletePassword(name);
                }
                db.Execute(sqlInsert, variables);
            }
            return password;
        }

        public static void DeletePassword(string name) {
            using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                UserPassword password = db.Query<UserPassword>("SELECT * FROM Passwords WHERE UserName=@Name", new { Name = name }).FirstOrDefault();

                string sqlInsert = "INSERT INTO OldPasswords([Password], [UserName], [TimeCreation], [TimeExpitation]) SELECT[Password], [UserName], [TimeCreation], [TimeExpitation] FROM Passwords WHERE Id = @Id";
                db.Execute(sqlInsert, new { Id = password.Id });

                string sqlDelete = "DELETE FROM Passwords WHERE Id = @Id";
                db.Execute(sqlDelete, new { Id = password.Id });
            }
        }

        public static void DeleteAllPasswords(string name) {
            using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                string sqlDelete = "DELETE FROM Passwords WHERE UserName = @Name";
                db.Execute(sqlDelete, new { Name = name });
                string sqlDeleteOld = "DELETE FROM OldPasswords WHERE UserName = @Name";
                db.Execute(sqlDeleteOld, new { Name = name });
            }
        }



        private static string CreatePassword() {
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
