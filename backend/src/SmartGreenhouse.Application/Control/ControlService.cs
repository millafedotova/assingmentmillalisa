using Microsoft.EntityFrameworkCore;
using SmartGreenhouse.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGreenhouse.Application.Control;

public class ControlService
{
    private readonly AppDbContext _db;
    private readonly ControlStrategySelector _selector;

    public ControlService(AppDbContext db, ControlStrategySelector selector)
    {
        _db = db;
        _selector = selector;
    }

    public async Task<IEnumerable<ActuatorCommand>> EvaluateAsync(int deviceId)
    {
        var readings = await _db.Readings
            .Where(r => r.DeviceId == deviceId)
            .GroupBy(r => r.SensorType)
            .Select(g => g.OrderByDescending(r => r.Timestamp).First())
            .ToListAsync();

        var (strategy, parameters) = await _selector.ResolveStrategyAsync(deviceId);

        var readingsDict = readings.ToDictionary(r => r.SensorType, r => r.Value);

        var context = new ControlContext
        {
            DeviceId = deviceId,
            Readings = readingsDict,
            Parameters = parameters
        };

        return await strategy.EvaluateAsync(context);
    }
}
