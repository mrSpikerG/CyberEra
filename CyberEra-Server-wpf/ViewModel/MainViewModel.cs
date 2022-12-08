using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.Core;
using CyberEra_Server_wpf.Model;
using CyberEra_Server_wpf.Model.DataBaseModels;
using CyberEra_Server_wpf.View;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace CyberEra_Server_wpf.ViewModel {
    public class MainViewModel : ObservableObject {

        internal List<Client> Users { get; private set; }
        internal Server Server { get; private set; }

        private string passwordMinutes;

        public string PasswordMinutes {
            get { return passwordMinutes; }
            set { passwordMinutes = value; }
        }


        private Model.Computer? selectedComputer;
        public Model.Computer? SelectedComputer {
            get { return selectedComputer; }
            set {
                selectedComputer = value;
                OnPropertyChanged("SelectedComputer");
            }
        }

        public ObservableCollection<Model.Computer> Computers { get; set; }
        public ObservableCollection<Model.Computer> Passwords { get; set; }

        private Visibility isPasswordHiden;
        public Visibility IsPasswordHiden {
            get { return isPasswordHiden; }
            set { isPasswordHiden = value; this.OnPropertyChanged("password hidden"); }
        }

        private Visibility isComputersHidden;
        public Visibility IsComputersHidden {
            get { return isComputersHidden; }
            set { isComputersHidden = value; this.OnPropertyChanged("computers hidden"); }
        }


        public MainViewModel() {
            LoggerController.Debug("create ViewModel");
            this.Computers = new ObservableCollection<Model.Computer>();
            this.Passwords = new ObservableCollection<Model.Computer>();
            this.Users = new List<Client>();
            this.Server = new Server();
            this.IsPasswordHiden = Visibility.Hidden;
            this.IsComputersHidden = Visibility.Visible;
            StartServer();
        }

        private RelayCommand changeComputerVisibility;
        public RelayCommand ChangeComputerVisibility {
            get {
                return changeComputerVisibility ??
                  (changeComputerVisibility = new RelayCommand(obj => {
                      this.IsPasswordHiden = Visibility.Hidden;
                      this.IsComputersHidden = Visibility.Visible;
                      this.OnPropertyChanged("");
                  }));
            }
        }

        private RelayCommand changePasswordVisibility;
        public RelayCommand ChangePasswordVisibility {
            get {
                return changePasswordVisibility ??
                  (changePasswordVisibility = new RelayCommand(obj => {

                      Task.Factory.StartNew(() => {
                          using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                              var password = db.Query<UserPassword>("SELECT * FROM Passwords");

                              Application.Current.Dispatcher.Invoke(new Action(() => {
                                  this.Passwords.Clear();
                                  foreach (var item in password) {
                                      this.Passwords.Add(new Model.Computer(item.UserName, "none", item.Password));
                                  }
                              }));
                          }
                      });

                      this.IsPasswordHiden = Visibility.Visible;
                      this.IsComputersHidden = Visibility.Hidden;
                      this.OnPropertyChanged("");
                  }));
            }
        }

        private RelayCommand openPasswordGiver;
        public RelayCommand OpenPasswordGiver {
            get {
                return openPasswordGiver ??
                  (openPasswordGiver = new RelayCommand(obj => {
                      if (this.SelectedComputer == null)
                          return;

                      if (Application.Current.Windows.OfType<PasswordGiver>().Any())
                          return;

                      this.PasswordMinutes = "Minutes";
                      PasswordGiver passwordGiver = new PasswordGiver(this);
                      passwordGiver.Show();
                  }));
            }
        }

        private RelayCommand sendPassword;
        public RelayCommand SendPassword {
            get {
                return sendPassword ??
                  (sendPassword = new RelayCommand(obj => {
                      if (this.SelectedComputer == null)
                          return;

                      if (this.PasswordMinutes == null)
                          return;

                      if (this.PasswordMinutes == "Minutes")
                          return;

                      try {
                          DateTime time = DateTime.Now.AddMinutes(int.Parse(this.PasswordMinutes));

                          Task.Factory.StartNew(() => {
                              PasswordController.GeneratePasswordByForce(this.SelectedComputer.ComputerName, time);
                              MessageBox.Show("Пароль успешно выдан", "Success");
                          });
                      } catch (Exception e) {
                          LoggerController.Error(e.Message);
                          LoggerController.Error(e.StackTrace);
                          return;
                      }
                  }));
            }
        }

        private RelayCommand kickCommand;
        public RelayCommand KickCommand {
            get {
                return kickCommand ??
                  (kickCommand = new RelayCommand(obj => {
                      if (this.SelectedComputer == null)
                          return;

                      LoggerController.Info($"Send END message to {this.SelectedComputer.Id}");
                      this.Users.First(x => x.Id == this.SelectedComputer.Id).sendMsg("END");
                  }));
            }
        }



        internal void StartServer() {
            Task.Factory.StartNew(() => {
                try {

                    this.Server.tcpListener.Start();
                    LoggerController.Info($"Server started");

                    while (true) {
                        TcpClient client = this.Server.tcpListener.AcceptTcpClient();
                        Task.Factory.StartNew(() => {
                            Client myClient = new Client(this, client);
                            myClient.StartClient();
                        });
                    }
                } catch (Exception e) {
                    MessageBox.Show(e.Message, "Exception");
                    LoggerController.Fatal(e.Message);
                    LoggerController.Fatal(e.StackTrace);
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

                Task.Factory.StartNew(() => {
                    try {
                        PasswordController.DeleteAllPasswords(client.PCName);
                        using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                            db.Execute("DELETE FROM Clients WHERE [Name] = @Name", new { Name = client.PCName });
                        }
                    } catch (Exception e) {
                        LoggerController.Error(e.Message);
                        LoggerController.Error(e.StackTrace);
                    }
                });

                return;
            }
        }

        internal void AddConnection(Client myClient) {
            this.Users.Add(myClient);
            Application.Current.Dispatcher.Invoke(new Action(() => { this.Computers.Add(new Model.Computer(myClient.PCName, myClient.Id)); }));
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
