using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartGreenhouse.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartGreenhouse.Application.Control;

public class ControlStrategySelector
{
    private readonly AppDbContext _db;
    private readonly IServiceProvider _provider;

    public ControlStrategySelector(AppDbContext db, IServiceProvider provider)
    {
        _db = db;
        _provider = provider;
    }

    public async Task<(IControlStrategy Strategy, Dictionary<string, object>? Parameters)> ResolveStrategyAsync(int deviceId)
    {
        var profile = await _db.ControlProfiles.FirstOrDefaultAsync(p => p.DeviceId == deviceId);
        Dictionary<string, object>? parameters = null;

        if (profile == null)
            return (_provider.GetRequiredService<HysteresisCoolingStrategy>(), null);

        if (!string.IsNullOrEmpty(profile.ParametersJson))
            parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(profile.ParametersJson!);

        IControlStrategy strategy = profile.StrategyKey switch
        {
            "MoistureTopUp" => _provider.GetRequiredService<MoistureTopUpStrategy>(),
            _ => _provider.GetRequiredService<HysteresisCoolingStrategy>()
        };

        return (strategy, parameters);
    }
}
