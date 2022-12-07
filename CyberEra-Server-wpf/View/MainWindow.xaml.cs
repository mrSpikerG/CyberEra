using CyberEra_Server_wpf.Control;
using CyberEra_Server_wpf.ViewModel;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberEra_Server_wpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {

            InitializeComponent();
            DataContext = new MainViewModel();
            this.Closing += MainWindowClosing;
        }

        private void MainWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e) {
            using (IDbConnection db = new SqlConnection(SettingsController.GetInstance().GetSettings().DBConnectionString)) {
                db.Equals("DELETE FROM Passwords");
                db.Equals("DELETE FROM OldPasswords");
                db.Equals("DELETE FROM Clients");
            }
        }
    }
}
