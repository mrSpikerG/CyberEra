using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class ComputerApp
    {
        public int? ComputerId { get; set; }
        public string? AppName { get; set; }
        public string? AppVersion { get; set; }

        public virtual Computer? Computer { get; set; }
    }
}
