using System.Net.Sockets;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System;
using System.Threading.Tasks;
using CyberEra_Server.Control;

namespace CyberEra_Server {
    public partial class ServerForm : Form {
        public ListBox ListBox { get; private set; }
        internal ServerController Controller { get; private set; }
        public UdpClient ServerUDP { get; private set; }
        public ServerForm() {
            InitializeComponent();
            this.Name = "Server";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;



            this.Controller = new ServerController(this);



            this.FormClosing += CloseConEvent;



            this.ListBox = new ListBox();
            this.ListBox.Items.Add("test1");
            this.ListBox.Items.Add("test1");
            this.ListBox.Items.Add("test1");
            this.ListBox.Location = new Point(12, 12);
            this.Controls.Add(ListBox);


        }

        private void CloseConEvent(object sender, FormClosingEventArgs e) {
            this.Controller.CloseServer();
        }
    }
}
