using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberEra_Server_wpf.ViewModel {
    public class MainViewModel : INotifyPropertyChanged {

        //   private ServerController? ServerControl;

        internal List<Client> Users { get; private set; }
        internal Server Server { get; private set; }

        private Computer? selectedComputer;
        public Computer? SelectedComputer {
            get { return selectedComputer; }
            set {
                selectedComputer = value;
                OnPropertyChanged("SelectedComputer");
            }
        }
        public ObservableCollection<Computer> Computers { get; set; }

        public MainViewModel() {
            LoggerController.Debug("create ViewModel");

            this.Computers = new ObservableCollection<Computer>();
            //  this.ServerControl = new ServerController(this);
            //  SingletoneGuiController.GetInstance().MainViewModel = this;

            this.Users = new List<Client>();
            this.Server = new Server();

            StartServer();

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        internal void StartServer() {


            Task.Factory.StartNew(() => {
                try {

                    this.Server.tcpListener.Start();
                    LoggerController.Info($"Server started");


                    //*   while (true) {
                    //  TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                    //ClientObject clientObject = new ClientObject(tcpClient, this);
                    //clients.Add(clientObject);
                    //Task.Run(clientObject.ProcessAsync);
                    // }//*

                    while (true) {
                        TcpClient client = this.Server.tcpListener.AcceptTcpClient();
                        Task.Factory.StartNew(() => {
                            Client myClient = new Client(this,client);
                            myClient.StartClient(); 
                        });
                        // Thread clientThread = new Thread(new ThreadStart(myClient.Work));
                        // clientThread.Start();
                    }


                } catch (Exception e) {
                    MessageBox.Show(e.Message + "\n" + e.StackTrace, "Exception");
                    LoggerController.Error(e.Message);
                    LoggerController.Error(e.StackTrace);
                } finally {
                    CloseServer();
                }
            });
        }


        internal void DeleteConnetion(string id) {
            Client client = this.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (client != null) {
                this.Users.Remove(client);
                Application.Current.Dispatcher.Invoke(new Action(() => { this.Computers.Remove(this.Computers.FirstOrDefault(x => x.Id.Equals(id))); }));
                LoggerController.Info($"remove connection [{client.Id}] {client.PCName}");
                return;
            }
        }

        internal void AddConnection(Client myClient) {
            this.Users.Add(myClient);
            Application.Current.Dispatcher.Invoke(new Action(() => { this.Computers.Add(new Computer(myClient.PCName, myClient.Id)); }));
            LoggerController.Info($"add connection [{myClient.Id}] {myClient.PCName}");

        }

        internal void CloseServer() {
            this.Server.tcpListener.Stop();
            for (int i = 0; i < this.Users.Count; i++) {
                this.Users[i].Close();
            }

        }
    }
}
