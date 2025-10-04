using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Enums;
using SmartGreenhouse.Infrastructure.Data;

public class CaptureReadingService
{
    private readonly AppDbContext _db;
    private readonly IDeviceFactoryResolver _resolver;

    public CaptureReadingService(AppDbContext db, IDeviceFactoryResolver resolver)
    {
        _db = db;
        _resolver = resolver;
    }

    public async Task<SensorReading> CaptureAsync(int deviceId, SensorTypeEnum sensorType, CancellationToken ct = default)
    {
        var device = await _db.Devices.FindAsync(deviceId);
        if (device == null) throw new Exception("Device not found");

        var factory = _resolver.Resolve(device);
        var reader = factory.CreateSensorReader();

        var raw = await reader.ReadAsync(deviceId, sensorType, ct);
        var normalized = SensorNormalizerFactory.Create(sensorType).Normalize(raw);

        var reading = new SensorReading
        {
            DeviceId = deviceId,
            SensorType = sensorType,
            Value = normalized,
            Unit = SensorNormalizerFactory.Create(sensorType).CanonicalUnit,
            Timestamp = DateTime.UtcNow
        };

        _db.SensorReadings.Add(reading);
        await _db.SaveChangesAsync(ct);

        return reading;
    }
}
