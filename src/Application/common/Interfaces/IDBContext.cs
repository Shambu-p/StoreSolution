using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.common.Interfaces
{
    public abstract class IDBContext: DbContext {
        
        public virtual DbSet<Box> Boxes { get; set; } = null!;
        public virtual DbSet<BoxItem> BoxItems { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreItem> StoreItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public IDBContext(DbContextOptions<IDBContext> options) : base(options) {
            
        }

        // public void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}