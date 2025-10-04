using SmartGreenhouse.Domain.Enums;

public record DeviceDto(int Id, string DeviceName, DeviceTypeEnum DeviceType, DateTime CreatedAt);
