using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Application.Abstractions;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;

namespace SmartGreenhouse.Application.Events.Observers;

public class AlertRuleObserver : IReadingObserver
{
    private readonly AppDbContext _db;

    public AlertRuleObserver(AppDbContext db) => _db = db;

    public async Task OnReadingAsync(IReadingEvent readingEvent)
    {
        var rules = await _db.AlertRules
            .Where(r => r.DeviceId == readingEvent.DeviceId
                        && r.SensorType == readingEvent.SensorType
                        && r.IsActive)
            .ToListAsync();

        foreach (var rule in rules)
        {
            bool triggered = rule.OperatorSymbol switch
            {
                ">" => readingEvent.Value > rule.Threshold,
                "<" => readingEvent.Value < rule.Threshold,
                "=" => Math.Abs(readingEvent.Value - rule.Threshold) < 1e-6,
                _ => false
            };

            if (triggered)
            {
                var notification = new AlertNotification
                {
                    DeviceId = readingEvent.DeviceId,
                    SensorType = readingEvent.SensorType,
                    Value = readingEvent.Value,
                    Timestamp = readingEvent.Timestamp,
                    AlertRuleId = rule.Id,
                    IsTriggered = true
                };

                _db.AlertNotifications.Add(notification);
            }
        }

        await _db.SaveChangesAsync();
    }
}
