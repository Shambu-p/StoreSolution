using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Infrastructure.Persistance.configuration
{
    public static class ItemConfiguration {
        
        public static ModelBuilder onBuild(ModelBuilder modelBuilder){

            modelBuilder.Entity<Item>(entity => {
                entity.ToTable("Item");

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Price).HasColumnType("double unsigned");
            });

            return modelBuilder;

        }
    }
}