using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberEra_Server.Model
{
    internal class Settings
    {
        public int Port { get; set; }
        public string IpAddress { get; set; }
        public string DBConnectionString { get; set; }
        public bool IsBlocked { get; set; }
    }
}
