using System.Text.Json.Serialization;

using SmartGreenhouse.Domain.Enums;
namespace SmartGreenhouse.Domain.Entities;
public class SensorReading
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public SensorTypeEnum SensorType { get; set; }  // заменили string на enum
    public double Value { get; set; }
    public string Unit { get; set; }
    public DateTime Timestamp { get; set; }

    public Device Device { get; set; }
}