using System;
using System.Collections.Generic;

namespace StoreSolution.Domain.Entity
{
    public partial class Box
    {
        public uint Id { get; set; }
        public uint StoreId { get; set; }
        public uint UserId { get; set; }

        public virtual Store Store { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
