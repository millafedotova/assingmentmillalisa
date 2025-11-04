namespace SmartGreenhouse.Api.Contracts;

public class UpsertAlertRuleRequest
{
    public int DeviceId { get; set; }
    public string SensorType { get; set; } = default!;
    public string OperatorSymbol { get; set; } = default!;
    public double Threshold { get; set; }
    public bool IsActive { get; set; } = true;
}
