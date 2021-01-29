using System;
using Microsoft.EntityFrameworkCore;
using SOATApiReact.Model;

namespace SOATApiReact.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        { 
            DataInitializer.Initialize(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .Property(e => e.DocumentType)
                .HasConversion(
                    d => d.ToString(),
                    d => (DocumentType)Enum.Parse(typeof(DocumentType), d));
            modelBuilder.Entity<SOAT>()
                .HasOne(s => s.Owner)
                .WithMany(o => o.SOATs)
                .HasForeignKey(s => s.OwnerDocument);
            modelBuilder.Entity<SOAT>()
                .HasOne(s => s.Vehicle)
                .WithMany(v => v.SOATs)
                .HasForeignKey(s => s.VehiclePlate);
            modelBuilder
                .Entity<SOAT>()
                .HasKey(s=> new {s.OwnerDocument, s.VehiclePlate, s.Year});
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SOAT> SOATs { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}