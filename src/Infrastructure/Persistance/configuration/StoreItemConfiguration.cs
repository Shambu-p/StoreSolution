using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Infrastructure.Persistance.configuration
{
    public static class StoreItemConfiguration {
        
        public static ModelBuilder onBuild(ModelBuilder modelBuilder) {
            
            modelBuilder.Entity<StoreItem>(entity => {

                entity.ToTable("StoreItem");

                entity.HasIndex(e => e.StoreId, "item_store");

                entity.HasIndex(e => e.ItemId, "stored_item");

                entity.HasOne(d => d.Item)
                    .WithMany(i => i.StoreItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stored_item");

                entity.HasOne(d => d.Store)
                    .WithMany(s => s.StoreItems)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_store");
            });

            return modelBuilder;

        }
    }
}