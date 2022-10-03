using System;
using System.Collections.Generic;

namespace StoreSolution.Domain.Entity {
    
    public partial class Store {

        public Store() {
            Boxes = new HashSet<Box>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public uint StoreKeeper { get; set; }

        public User StoreKeeperNavigation { get; set; } = null!;
        public ICollection<Box> Boxes { get; set; }
        public List<StoreItem> StoreItems {get; set;} = null!;

    }

}
