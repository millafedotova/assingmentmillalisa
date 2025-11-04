using SmartGreenhouse.Application.Abstractions;

namespace SmartGreenhouse.Application.Events.Observers;

public class LogObserver : IReadingObserver
{
    public Task OnReadingAsync(IReadingEvent e)
    {
        Console.WriteLine($"[LogObserver] {e.Timestamp}: Device {e.DeviceId} {e.SensorType}={e.Value}");
        return Task.CompletedTask;
    }
}
