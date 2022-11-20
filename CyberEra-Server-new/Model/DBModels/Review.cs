using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class Review
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short? Stars { get; set; }
    }
}
