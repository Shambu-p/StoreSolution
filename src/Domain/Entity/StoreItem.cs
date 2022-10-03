using System;
using System.Collections.Generic;

namespace StoreBackendClean.Domain.Entity
{
    public partial class StoreItem
    {
        public uint Id {get; set;}
        public uint StoreId { get; set; }
        public uint ItemId { get; set; }
        public uint TotalAmount { get; set; }
        public uint UnboxedAmount { get; set; }

        public Item Item { get; set; }
        public Store Store { get; set; }
    }
}
