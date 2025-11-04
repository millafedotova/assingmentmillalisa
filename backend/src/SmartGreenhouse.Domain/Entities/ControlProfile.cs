namespace SmartGreenhouse.Domain.Entities;

public class ControlProfile
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public string StrategyKey { get; set; } = default!;
    public string? ParametersJson { get; set; }
}
