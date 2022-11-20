using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class ComputersPassword
    {
        public int? ComputerId { get; set; }
        public int? UserId { get; set; }
        public DateTime? TimeCreation { get; set; }
        public DateTime? TimeForPlaying { get; set; }
        public string? Password { get; set; }

        public virtual Computer? Computer { get; set; }
        public virtual User? User { get; set; }
    }
}
