using CyberEra_Server.Model;
using NLog.Fluent;
using System;
using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

namespace CyberEra_Server.Control
{
    internal class ServerController
    {

        internal List<Client> Users { get; private set; }
        private ServerForm Form;
        internal Server Server { get; private set; }

       

        public ServerController(ServerForm form)
        {
            this.Users = new List<Client>();
            this.Form = form;
            this.Server = new Server();
           StartServer();
        }


        internal void StartServer() {
            
            try {
                Log.Info("Server started");
                this.Server.tcpListener.Start();
                Log.Warn("Sd");
                while (true) {
                   
                   // Client myClient = new Client(this);
                   

                }

            } catch (Exception e) {
                ErrorController.ShowError("Server runtime error",e.Message);
                CloseServer();
            }
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
            Log.Info($"add connection {myClient.PCName}");
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
