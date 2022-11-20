using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class ComputersLastActivity
    {
        public int? ComputerId { get; set; }
        public DateTime? TimeActivity { get; set; }

        public virtual Computer? Computer { get; set; }
    }
}
