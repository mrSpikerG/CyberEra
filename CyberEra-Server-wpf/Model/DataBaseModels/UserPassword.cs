using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static log4net.Appender.RollingFileAppender;

namespace CyberEra_Server_wpf.Model.DataBaseModels {
    class UserPassword {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime TimeCreation { get; set; }
        public DateTime TimeExpitation { get; set; }
    }
}
