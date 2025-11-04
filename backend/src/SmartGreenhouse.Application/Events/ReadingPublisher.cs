using SmartGreenhouse.Application.Abstractions;

namespace SmartGreenhouse.Application.Events;

public class ReadingPublisher : IReadingPublisher
{
    private readonly IEnumerable<IReadingObserver> _observers;

    public ReadingPublisher(IEnumerable<IReadingObserver> observers)
    {
        _observers = observers;
    }

    public async Task PublishAsync(IReadingEvent readingEvent)
    {
        foreach (var observer in _observers)
        {
            await observer.OnReadingAsync(readingEvent);
        }
    }
}
