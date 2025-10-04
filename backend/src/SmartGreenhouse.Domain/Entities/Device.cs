using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Enums;

public class Device
{
    public int Id { get; set; }
    public required string DeviceName { get; set; }
    public DeviceTypeEnum DeviceType { get; set; } = DeviceTypeEnum.Simulated;
    public DateTime CreatedAt { get; set; }

    public ICollection<SensorReading> Readings { get; set; }
}
