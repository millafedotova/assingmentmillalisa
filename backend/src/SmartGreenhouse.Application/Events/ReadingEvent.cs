using SmartGreenhouse.Application.Abstractions;

namespace SmartGreenhouse.Application.Events;

public record ReadingEvent(int DeviceId, string SensorType, double Value, DateTime Timestamp)
    : IReadingEvent;
