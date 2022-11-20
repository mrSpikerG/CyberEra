using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class Zone
    {
        public Zone()
        {
            Computers = new HashSet<Computer>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
