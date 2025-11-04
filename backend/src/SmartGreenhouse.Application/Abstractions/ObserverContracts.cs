namespace SmartGreenhouse.Application.Abstractions;

public interface IReadingEvent
{
    int DeviceId { get; }
    string SensorType { get; }
    double Value { get; }
    DateTime Timestamp { get; }
}

public interface IReadingObserver
{
    Task OnReadingAsync(IReadingEvent readingEvent);
}

public interface IReadingPublisher
{
    Task PublishAsync(IReadingEvent readingEvent);
}
