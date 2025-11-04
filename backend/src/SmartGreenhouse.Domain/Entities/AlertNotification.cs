namespace SmartGreenhouse.Domain.Entities;

public class AlertNotification
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public string SensorType { get; set; } = default!;
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
    public int? AlertRuleId { get; set; }
    public bool IsTriggered { get; set; } = true;
}
