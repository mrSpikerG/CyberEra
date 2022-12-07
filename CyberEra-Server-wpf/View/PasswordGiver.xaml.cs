using CyberEra_Server_wpf.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CyberEra_Server_wpf.View {
    /// <summary>
    /// Логика взаимодействия для PasswordGiver.xaml
    /// </summary>
    public partial class PasswordGiver : Window {
        public PasswordGiver(MainViewModel viewModel) {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
