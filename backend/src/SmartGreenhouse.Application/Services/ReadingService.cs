using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Infrastructure.Data;
using SmartGreenhouse.Application.Abstractions; 
using SmartGreenhouse.Application.Events;       

namespace SmartGreenhouse.Application.Services;

public class ReadingService
{
    private readonly AppDbContext _db;
    private readonly IReadingPublisher _publisher; 

    public ReadingService(AppDbContext db, IReadingPublisher publisher)
    {
        _db = db;
        _publisher = publisher;
    }

    public async Task<IReadOnlyList<SensorReading>> QueryAsync(
        int? deviceId = null,
        string? sensorType = null,
        int take = 200
    )
    {
        var q = _db.Readings.AsNoTracking().OrderByDescending(r => r.Timestamp).AsQueryable();

        if (deviceId.HasValue)
            q = q.Where(r => r.DeviceId == deviceId.Value);

        if (!string.IsNullOrWhiteSpace(sensorType))
            q = q.Where(r => r.SensorType == sensorType);

        return await q.Take(take).ToListAsync();
    }

    public async Task<SensorReading> AddAsync(SensorReading reading)
    {
        _db.Readings.Add(reading);
        await _db.SaveChangesAsync();

        
        await _publisher.PublishAsync(new ReadingEvent(
            reading.DeviceId,
            reading.SensorType,
            reading.Value,
            reading.Timestamp
        ));

        return reading;
    }
}
