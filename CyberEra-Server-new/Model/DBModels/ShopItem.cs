using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public float? Cost { get; set; }
        public int? Count { get; set; }
        public string? ImageSrc { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
