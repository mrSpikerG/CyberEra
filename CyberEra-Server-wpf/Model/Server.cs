

using CyberEra_Server_wpf.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CyberEra_Server_wpf.Model {
    internal class Server {
        

        public TcpListener tcpListener { get; private set; }
        public Server() {
            IPAddress address = IPAddress.Parse(SettingsController.GetInstance().GetSettings().IpAddress);
            this.tcpListener = new TcpListener(address, SettingsController.GetInstance().GetSettings().Port);
        }

     
    }
}
