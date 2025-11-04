using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<SensorReading> Readings => Set<SensorReading>();
    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
    public DbSet<AlertNotification> AlertNotifications => Set<AlertNotification>();
    public DbSet<ControlProfile> ControlProfiles => Set<ControlProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>().HasKey(d => d.Id);
        modelBuilder.Entity<Device>().Property(d => d.DeviceName).HasMaxLength(120);
        modelBuilder.Entity<Device>().HasIndex(d => d.DeviceName);

        modelBuilder.Entity<SensorReading>().HasKey(r => r.Id);
        modelBuilder.Entity<SensorReading>()
            .HasOne(r => r.Device)
            .WithMany(d => d.Readings)
            .HasForeignKey(r => r.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AlertRule>().HasKey(r => r.Id);
        modelBuilder.Entity<AlertRule>().HasIndex(r => new { r.DeviceId, r.SensorType });

        modelBuilder.Entity<AlertNotification>().HasKey(n => n.Id);
        modelBuilder.Entity<AlertNotification>().HasIndex(n => new { n.DeviceId, n.SensorType, n.Timestamp });

        modelBuilder.Entity<ControlProfile>().HasKey(c => c.Id);
        modelBuilder.Entity<ControlProfile>().HasIndex(c => c.DeviceId);
    }
}
