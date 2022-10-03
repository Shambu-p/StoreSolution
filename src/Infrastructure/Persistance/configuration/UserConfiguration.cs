using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Infrastructure.Persistance.configuration
{
    public static class UserConfiguration {
        
        public static ModelBuilder onBuild(ModelBuilder modelBuilder){

            modelBuilder.Entity<User>(entity => {

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "email")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasColumnType("text")
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

            });

            return modelBuilder;

        }

    }
}