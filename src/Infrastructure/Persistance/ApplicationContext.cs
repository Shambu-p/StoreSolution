using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreSolution.Domain.Entity;
using StoreSolution.Infrastructure.Persistance.configuration;
using StoreSolution.Application.common.Interfaces;

namespace StoreSolution.Infrastructure.Persistance {

    public partial class ApplicationContext : IDBContext {

        // public ApplicationContext() {}

        public ApplicationContext(DbContextOptions<IDBContext> options) : base(options) {
            
        }

        public virtual DbSet<Box> Boxes { get; set; } = null!;
        public virtual DbSet<BoxItem> BoxItems { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreItem> StoreItems { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Box>(entity =>
            {
                entity.ToTable("Box");

                entity.HasIndex(e => e.UserId, "box_creator");

                entity.HasIndex(e => e.StoreId, "box_store");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("box_store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("box_creator");
            });

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

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Price).HasColumnType("double unsigned");
            });

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

            modelBuilder.Entity<StoreItem>(entity =>
            {

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

            modelBuilder.Entity<User>(entity =>
            {
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

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}
