
using Azure;
using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.Model.DataBaseModels;
using CyberEra_Server_wpf.ViewModel;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CyberEra_Server_wpf.Model {
    internal class Client {
        public string Id { get; private set; }
        public string PCName { get; private set; }



        public MainViewModel ServerView { get; protected set; }

        public NetworkStream? NetworkStream { get; protected set; }

        private TcpClient TcpClientVariable;




        public Client(MainViewModel server, TcpClient client, string name = "unnamed") {
            this.PCName = name;
            this.Id = Guid.NewGuid().ToString();
            this.ServerView = server;
            this.TcpClientVariable = client;
            this.NetworkStream = client.GetStream();
            server.AddConnection(this);
        }

        public void StartClient() {
            try {
                var response = new List<byte>();
                while (true) {

                    string cmd = GetMsg();
                    if (cmd == "")
                        continue;

                    if (cmd.Equals("END")) {
                        break;
                    }

                    LoggerController.Info($"recieve msg from client {cmd}");
                    CommandBase command = JsonSerializer.Deserialize<CommandBase>(cmd);

                    switch (command.CommandName) {
                        case "setName":

                            //string newName = command.CommandArgs;
                            //while()
                                this.PCName = command.CommandArgs;
                            try {
                                while(true){
                                    var user = this.ServerView.Users.First(x => ((x.PCName.Equals(this.PCName)) &&(x.Id!=this.Id)));
                                    this.PCName += "_new";
                                }
                            } catch {
                            }


                            this.ServerView.Computers.FirstOrDefault(x => x.Id.Equals(this.Id)).ComputerName = this.PCName;

                            Task.Factory.StartNew(() => {
                                try {
                                    using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                                        db.Execute("INSERT INTO Clients([Name]) VALUES (@Name)", new { Name = this.PCName });
                                    }


                                    PasswordSchedule();
                                } catch (Exception e) {
                                    LoggerController.Error(e.Message);
                                    LoggerController.Error(e.StackTrace);
                                }
                            });
                            break;
                    }


                }
            } catch (Exception e) {
                MessageBox.Show(e.Message, "Exception");
                LoggerController.Fatal(e.Message);
                LoggerController.Fatal(e.StackTrace);
            } finally {
                this.ServerView.DeleteConnetion(this.Id);
                this.Close();
            }
        }

        public bool sendMsg(string Message) {

            try {
                byte[] buffer = Encoding.Unicode.GetBytes(Message);
                LoggerController.Info($"Send to {this.Id} message {Message}");
                this.NetworkStream.WriteAsync(buffer, 0, buffer.Length);

            } catch (Exception e) {
                LoggerController.Error(e.Message);
                LoggerController.Error(e.StackTrace);
                return false;
            }

            return true;
        }

        public string GetMsg() {
            byte[] data = new byte[4096];
            StringBuilder builder = new StringBuilder();
            int byteCount = 0;
            do {
                byteCount = NetworkStream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
            } while (NetworkStream.DataAvailable);

            return builder.ToString();
        }


        public void PasswordSchedule() {
            while (true) {

                PasswordController.TryGenerateNewPassword(this.PCName, DateTime.Now.AddMinutes(20));

                using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {

                    UserPassword pass = db.Query<UserPassword>("SELECT * FROM Passwords WHERE UserName=@Name", new { Name = this.PCName }).FirstOrDefault();
                    CommandBase command = new CommandBase("checkPassword", pass.Password);
                    this.sendMsg(JsonSerializer.Serialize(command));
                }
                Thread.Sleep(60 * 1000);

            }
        }


        public void Close() {
            NetworkStream?.Close();
        }
    }
}
