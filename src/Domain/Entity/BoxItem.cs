using System;
using System.Collections.Generic;

namespace StoreSolution.Domain.Entity
{
    public partial class BoxItem
    {
        public uint Id {get; set;}
        public uint BoxId { get; set; }
        public uint ItemId { get; set; }
        public int Amount { get; set; }

        public virtual Box Box { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
