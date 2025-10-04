using SmartGreenhouse.Domain.Enums;

public record CaptureReadingRequest(int DeviceId, SensorTypeEnum SensorType);
