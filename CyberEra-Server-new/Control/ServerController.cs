using CyberEra_Server.Model;
using NLog.Fluent;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberEra_Server.Control {
    internal class ServerController {

        internal List<Client> Users { get; private set; }
        private ServerForm Form;
        internal Server Server { get; private set; }



        public ServerController(ServerForm form) {
            this.Users = new List<Client>();
            this.Form = form;
            Log.Info("1");
            this.Server = new Server();
            Log.Info("2");
            StartServer();
            Log.Info("3");
        }


        internal void StartServer() {


            Task.Factory.StartNew(() => {
                try {
                    Log.Info("Server started");
                    this.Server.tcpListener.Start();



                 /*   while (true) {
                        TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                        //ClientObject clientObject = new ClientObject(tcpClient, this);
                        //clients.Add(clientObject);
                        //Task.Run(clientObject.ProcessAsync);
                    }*/

                    while (true) {
                        TcpClient client = this.Server.tcpListener.AcceptTcpClient();
                        Client myClient = new Client(this);
                        // Thread clientThread = new Thread(new ThreadStart(myClient.Work));
                        // clientThread.Start();
                    }


                } catch (Exception e) {
                    ErrorController.ShowError("Server runtime error", e.Message);
                } finally {
                    CloseServer();
                }
            });
        }


        internal void DeleteConnetion(string id) {
            Client client = this.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (client != null) {
                this.Users.Remove(client);
                Log.Info($"remove connection {client.PCName}");
                return;
            }
        }

        internal void AddConnection(Client myClient) {
            this.Users.Add(myClient);
            this.Form.ListBox.Items.Add("user");
           
        }

        internal void CloseServer() {
            this.Server.tcpListener.Stop();
            for (int i = 0; i < this.Users.Count; i++) {
                this.Users[i].Close();
            }
            Log.Info("Server stop");
        }
    }
}
