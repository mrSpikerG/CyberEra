using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class Computer
    {
        public int Id { get; set; }
        public int? ZoneId { get; set; }
        public string? Name { get; set; }

        public virtual Zone? Zone { get; set; }
    }
}
