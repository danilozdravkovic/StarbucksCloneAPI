using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarbuckClone.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksClone.DataAccess
{
    public class SCContext : DbContext
    {
        private readonly string _connectionString;

        public SCContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SCContext()
        {
            _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=StarbucksClone;TrustServerCertificate=true;Integrated security = true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<Size>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Size>().HasIndex(x => x.Name).IsUnique();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartLine>().Property(x => x.SizeVolume).IsRequired().HasMaxLength(20);


            modelBuilder.Entity<Order>().Property(x => x.Address).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Order>().Property(x => x.TotalPrice).IsRequired();

            modelBuilder.Entity<OrderLine>().Property(x => x.SizeVolume).IsRequired().HasMaxLength(20);
            

            modelBuilder.Entity<OrderLineAddIn>().Property(x => x.AddIn).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<LinkPosition>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<LinkPosition>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<UseCasesAuditLog>().Property(x => x.Username).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<UseCasesAuditLog>().Property(x => x.UseCaseName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Role>().Property(x => x.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<UserUseCase>().HasKey(x => new
            {
                x.UserId,
                x.UseCaseId
            });

            modelBuilder.Entity<RoleUseCase>().HasKey(x => new
            {
                x.RoleId,
                x.UseCaseId
            });
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<CartLinesAddIn> CartLinesAddIns { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<OrderLineAddIn> OrderLinesAddIns { get; set; }
        public DbSet<NavigationLink> NavigationLinks { get; set; }
        public DbSet<LinkPosition> LinkPositions { get; set; }
        public DbSet<UseCasesAuditLog> UseCasesAuditLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AddIn> AddIns { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }

    }
}
