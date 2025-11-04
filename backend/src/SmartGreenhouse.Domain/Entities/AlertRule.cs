namespace SmartGreenhouse.Domain.Entities;

public class AlertRule
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public string SensorType { get; set; } = default!;
    public string OperatorSymbol { get; set; } = default!; // ">", "<", "="
    public double Threshold { get; set; }
    public bool IsActive { get; set; } = true;
}
