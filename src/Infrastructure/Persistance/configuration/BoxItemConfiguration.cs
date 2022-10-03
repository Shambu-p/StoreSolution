using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Infrastructure.Persistance.configuration
{
    public static class BoxItemConfiguration {
        
        public static ModelBuilder onBuild(ModelBuilder modelBuilder){
            
            modelBuilder.Entity<BoxItem>(entity => {

                entity.HasIndex(e => e.BoxId, "box");

                entity.HasIndex(e => e.ItemId, "item");

                entity.HasOne(d => d.Box)
                    .WithMany()
                    .HasForeignKey(d => d.BoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("box");

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item");
            });

            return modelBuilder;

        }
    }
}