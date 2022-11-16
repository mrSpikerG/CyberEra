using CyberEra_Server.Control;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberEra_Server.Model {
    internal class Server {
        
        private Logger Log = LogManager.GetCurrentClassLogger();
        public TcpListener tcpListener { get; private set; }
        public Server() {
            IPAddress address = IPAddress.Parse(SettingsController.GetInstance().GetSettings().IpAddress);
            this.tcpListener = new TcpListener(address, SettingsController.GetInstance().GetSettings().Port);
        }

     
    }
}
