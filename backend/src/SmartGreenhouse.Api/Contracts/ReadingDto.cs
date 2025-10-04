using SmartGreenhouse.Domain.Enums;

public record ReadingDto(int Id, int DeviceId, SensorTypeEnum SensorType, double Value, string Unit, DateTime Timestamp);
