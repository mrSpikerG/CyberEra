using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace CyberEra_Client {
    public partial class ClientForm : Form {
        
        private static TcpClient client = new TcpClient();
        private static NetworkStream stream = null;
        
        public ClientForm() {
            InitializeComponent();


            const int PORT = 8888;
            const string HOST = "127.0.0.1";


//            Console.Write("Enter name:\t");
            //string userName = Console.ReadLine();

            //Console.Title = userName;

            try {
                client.Connect(HOST, PORT);
                stream = client.GetStream();

                //
                //  send name
                //
                byte[] buffer = Encoding.Unicode.GetBytes("");
                stream.Write(buffer, 0, buffer.Length);

                //
                //  send adress
                //
                byte[] adr = Encoding.Unicode.GetBytes(GetLocalIPAddress());
                stream.Write(adr, 0, adr.Length);

              //  Thread receiveMsgThread = new Thread(ReceiveMsg);
              //  receiveMsgThread.Start();
              //  Console.WriteLine($"Welcome, {userName}");
                SendMsg();

            } catch (Exception ex) {
                //   Console.WriteLine(ex.Message);
                File.WriteAllText("logs","");
                File.AppendAllText("logs", ex.Message);
            } finally {
                Disconnect();
            }
           // Console.ReadKey();
        }


        private static void SendMsg() {
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
        }

        private static void ReceiveMsg() {
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

        private static void Disconnect() {
            stream.Close();
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

