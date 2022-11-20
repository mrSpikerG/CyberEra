using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
