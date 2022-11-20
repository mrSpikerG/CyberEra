using CyberEra_Server.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Server.Model {
    internal class Client {
        public string Id { get; private set; }
        public string PCName { get; private set; }


        public Server _Server { get; protected set; }

        public NetworkStream NetworkStream { get; protected set; }


        public Client(ServerController server, string name = "unnamed") {
            this.PCName = name;
            this.Id = Guid.NewGuid().ToString();
            this._Server = server.Server;
            server.AddConnection(this);
        }

        public void StartClient() {

        }

        public void Close() {
            this.NetworkStream?.Close();
        }
    }
}
