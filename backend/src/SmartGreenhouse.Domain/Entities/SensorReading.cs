namespace SmartGreenhouse.Domain.Entities;

public class SensorReading
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device? Device { get; set; }
    public string SensorType { get; set; } = "";
    public double Value { get; set; }
    public string Unit { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
