using SmartGreenhouse.Domain.Enums;

public interface ISensorReader
{
    Task<double> ReadAsync(int deviceId, SensorTypeEnum sensorType, CancellationToken ct = default);
    string UnitFor(SensorTypeEnum sensorType);
}
