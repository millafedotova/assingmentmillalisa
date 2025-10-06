using SmartGreenhouse.Domain.Enums;

namespace SmartGreenhouse.Api.Contracts;
public record CaptureReadingRequest(int DeviceId, SensorTypeEnum SensorType);
