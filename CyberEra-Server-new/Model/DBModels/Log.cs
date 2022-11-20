using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class Log
    {
        public int? Id { get; set; }
        public string? LogContext { get; set; }
        public DateTime? TimeCreation { get; set; }
    }
}
