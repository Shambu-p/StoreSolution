using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Infrastructure.Persistance.configuration
{
    public static class StoreConfiguration {
        
        public static ModelBuilder onBuild(ModelBuilder modelBuilder) {
            
            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.HasIndex(e => e.StoreKeeper, "store_keeper");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.StoreKeeperNavigation)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreKeeper)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("store_keeper");
            });

            return modelBuilder;

        }

    }
}