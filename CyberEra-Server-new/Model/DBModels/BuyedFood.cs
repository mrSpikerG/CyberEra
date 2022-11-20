using System;
using System.Collections.Generic;

namespace CyberEra_Server_new.Model.DBModels
{
    public partial class BuyedFood
    {
        public int? Id { get; set; }
        public int? ShopItemId { get; set; }
        public int? ComputerId { get; set; }
        public DateTime? TimeCreation { get; set; }
        public DateTime? TimeForPlaying { get; set; }

        public virtual Computer? Computer { get; set; }
        public virtual ShopItem? ShopItem { get; set; }
    }
}
