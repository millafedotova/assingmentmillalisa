namespace SmartGreenhouse.Api.Contracts;

public class SetControlProfileRequest
{
    public int DeviceId { get; set; }
    public string StrategyKey { get; set; } = default!;
    public Dictionary<string, object>? Parameters { get; set; }
}
