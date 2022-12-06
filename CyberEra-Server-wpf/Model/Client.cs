
using Azure;
using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.ViewModel;
using System;
using System.Collections.Generic;
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

                    if (cmd == "END")
                        break;


                    


                    LoggerController.Info($"recieve msg from client {cmd}");
                    CommandBase command = JsonSerializer.Deserialize<CommandBase>(cmd);

                    switch (command.CommandName) {
                        case "setName":
                            this.PCName = command.CommandArgs;
                            this.ServerView.Computers.FirstOrDefault(x => x.Id.Equals(this.Id)).ComputerName = this.PCName;
                            break;
                    }


                }
            } catch (Exception e) {
                MessageBox.Show(e.Message + "\n" + e.StackTrace, "Exception");
                LoggerController.Error(e.Message);
                LoggerController.Error(e.StackTrace);
            } finally {
                this.ServerView.DeleteConnetion(this.Id);
                this.Close();
            }
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
            CommandBase command = new CommandBase("checkPassword","passw");
            Thread.Sleep(60000);
        }


        public void Close() {
            NetworkStream?.Close();
        }
    }
}
