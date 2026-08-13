using System.Collections.Generic;
using BikeStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStoreApp.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.\\SQLEXPRESS;" +
                    "Database=BikeStores;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.BrandId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Orders)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.OrderId,
                    e.ItemId
                });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffId);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staff);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.StoreId,
                    e.ProductId
                });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}