using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class ComputersIp
    {
        public int? ComputerId { get; set; }
        public string? IpAddress { get; set; }
        public int? Version { get; set; }

        public virtual Computer? Computer { get; set; }
    }
}
