using System;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Enums;
using SmartGreenhouse.Infrastructure.Data;

namespace SmartGreenhouse.Application.Services;

public class ReadingService
{
    private readonly AppDbContext _db;
    public ReadingService(AppDbContext db) => _db = db;

    public async Task<List<SensorReading>> QueryAsync(int? deviceId = null, SensorTypeEnum? sensorType = null, int take = 200)
{
    var query = _db.SensorReadings.AsQueryable();
    if (deviceId.HasValue) query = query.Where(r => r.DeviceId == deviceId);
    if (sensorType.HasValue) query = query.Where(r => r.SensorType == sensorType.Value);
    return await query.OrderByDescending(r => r.Timestamp).Take(take).ToListAsync();
}


    public async Task<SensorReading> AddAsync(SensorReading reading)
    {
        _db.SensorReadings.Add(reading);
        await _db.SaveChangesAsync();
        return reading;
    }
}