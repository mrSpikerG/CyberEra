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

namespace CyberEra_Client {
    public partial class ClientForm : Form {

        private static TcpClient client = new TcpClient();
        private static NetworkStream stream = null;
        private static WindowsControl WindowsController = new WindowsControl();



        public ClientForm() {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
            this.FormClosing += new FormClosingEventHandler(Form1_Closing);


            // SystemInformation.UserName - имя пользователя

            Label testLabel = new Label();
            testLabel.Location = new Point(100, 100);
            testLabel.AutoSize = true;
            // DirectoryEntry theEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
            // UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName))

            PasswordControl passwordControl = new PasswordControl();

            testLabel.Text = passwordControl.HasOldPassword("Vasya").ToString();

            //DirectoryEntry user = theEntry.Children.Add(SystemInformation.UserName, "user");

            //WindowsIdentity.GetCurrent().

            //  const int WTS_CURRENT_SESSION = -1;
            // IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;


            //if (!passwordControl.HasOldPassword("Vasya")) {
            //    if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, false))
            //        throw new Win32Exception();
            //}
            this.Controls.Add(testLabel);


            const int PORT = 8888;
            const string HOST = "127.0.0.1";
            Random rand = new Random();
            try {
                client.Connect(HOST, PORT);
                stream = client.GetStream();

                //
                //  send name
                //

                CommandBase command = new CommandBase("setName", Environment.MachineName + rand.Next(0, 1000));


                byte[] buffer = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(command));
                LoggerControl.Info($"Send to server {Environment.MachineName}");
                stream.WriteAsync(buffer, 0, buffer.Length);

                

            } catch (Exception ex) {
                LoggerControl.Error(ex.Message);
                LoggerControl.Error(ex.StackTrace);
            } finally {

            }
            // Console.ReadKey();


        }



        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            byte[] buffer = Encoding.Unicode.GetBytes("END");
            stream.Write(buffer, 0, buffer.Length);
            LoggerControl.Info($"Send to server END");
            stream.WriteAsync(buffer, 0, buffer.Length);
            Disconnect();
        }

        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern int WTSGetActiveConsoleSessionId();





        /*   private static void SendMsg() {
               Console.Write("Enter msg:\t");
               while (true) {
                   //
                   //  send message
                   //
                   byte[] data = Encoding.Unicode.GetBytes(Console.ReadLine());
                   stream.Write(data, 0, data.Length);

                   //
                   //  send time
                   //
                   byte[] data2 = Encoding.Unicode.GetBytes(DateTime.Now.ToString());
                   stream.Write(data2, 0, data2.Length);


               }
           }*/

        /* private static void ReceiveMsg() {
             while (true) {
                 try {
                     byte[] data = new byte[256];
                     StringBuilder builder = new StringBuilder();
                     int byteCount = 0;
                     do {
                         byteCount = stream.Read(data, 0, data.Length);
                         builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
                     } while (stream.DataAvailable);

                     Console.WriteLine(builder.ToString());
                 } catch (Exception ex) {
                     Console.WriteLine(ex.Message);
                     Disconnect();
                     Environment.Exit(0);
                 }
             }
         }
 */
        private static void Disconnect() {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }



        public static string GetLocalIPAddress() {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}

