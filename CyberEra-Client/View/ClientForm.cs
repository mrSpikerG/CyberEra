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
using CyberEra_Client.Control;
using System.Runtime.InteropServices;

namespace CyberEra_Client {
    public partial class ClientForm : Form {

        private static TcpClient client = new TcpClient();
        private static NetworkStream stream = null;
        private static WindowsControl WindowsController = new WindowsControl();


        public ClientForm() {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.WorkingArea;
            // FormClosing += WindowsController.Client_FormClosing_deny;
            WindowsController.SetAutorunValue(false);
            const int x = 32000;
            const int y = 32000;
            //Put mouse in the screen ceneter and click its right button to prevent ability
            //of taskbar
            


int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);

            mouse_event(MouseFlags.Absolute | MouseFlags.Move, x, y, 0, UIntPtr.Zero);
                    mouse_event(MouseFlags.Absolute | MouseFlags.RightDown, x, y, 0, UIntPtr.Zero);
                    mouse_event(MouseFlags.Absolute | MouseFlags.RightUp, x, y, 0, UIntPtr.Zero);
              
            //const int PORT = 8888;
            //const string HOST = "127.0.0.1";
            /*
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

                        //    SendMsg();

                        } catch (Exception ex) {
                            //   Console.WriteLine(ex.Message);
                            File.WriteAllText("logs","");
                            File.AppendAllText("logs", ex.Message);
                        } finally {
                            Disconnect();
                        }*/
            // Console.ReadKey();
        }



        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;



        [DllImport("User32.dll")]
        static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        //Mouse flags enum
        [Flags]
        enum MouseFlags {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            Absolute = 0x8000
        };

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

