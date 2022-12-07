using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Server_wpf.Model {
    public class Computer : INotifyPropertyChanged {

        private string? computerName;
        private string? timeOfUpdating;
        private string? id;


        public string? Id {
            get { return id; }
            set { id = value; }
        }
        public string? ComputerName {
            get { return computerName; }
            set {
                computerName = value;
                OnPropertyChanged("computerName");
            }
        }
        public string? TimeOfUpdating {
            get { return timeOfUpdating; }
            set {
                timeOfUpdating = value;
                OnPropertyChanged("timeOfUpdating");
            }
        }


        public Computer(string? name, string? id) {
            this.ComputerName = name;
            this.Id = id;
            this.UpdateTime();
        }

        public Computer(string? name, string? id,string? time) {
            this.ComputerName = name;
            this.Id = id;
            this.timeOfUpdating= time;
        }

        public int MyProperty { get; set; }
        public void UpdateTime() {
            
            this.TimeOfUpdating = DateTime.Now.ToLongTimeString();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
