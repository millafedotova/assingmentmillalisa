namespace SmartGreenhouse.Domain.Entities;

public class Device
{
    public int Id { get; set; }
    public string DeviceName { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<SensorReading> Readings { get; set; } = new List<SensorReading>();
}
