using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SensorReading> SensorReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasKey(d => d.Id);
            modelBuilder.Entity<Device>().Property(d => d.DeviceName).HasMaxLength(120);
            modelBuilder.Entity<Device>().HasIndex(d => d.DeviceName);
            modelBuilder.Entity<Device>().HasIndex(d => d.DeviceType);

            modelBuilder.Entity<SensorReading>().HasKey(r => r.Id);
            modelBuilder.Entity<SensorReading>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Readings)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SensorReading>()
                .HasIndex(r => new { r.DeviceId, r.SensorType, r.Timestamp });
        }
    }
}
