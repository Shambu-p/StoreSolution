using System;
using System.Collections.Generic;

namespace StoreBackendClean.Domain.Entity
{
    public partial class User
    {
        public User() {
            Boxes = new HashSet<Box>();
            Stores = new List<Store>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte Role { get; set; }

        public virtual ICollection<Box> Boxes { get; set; }
        public List<Store> Stores { get; set; }
    }
}
