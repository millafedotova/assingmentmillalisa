using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using SmartGreenhouse.Application.Control;

namespace SmartGreenhouse.Application.Control;

public class HysteresisCoolingStrategy : IControlStrategy
{
    public Task<IEnumerable<ActuatorCommand>> EvaluateAsync(ControlContext context)
    {
        var readings = context.Readings ?? new Dictionary<string, double>();
        var parameters = context.Parameters ?? new Dictionary<string, object>();

        double onAbove = parameters.TryGetValue("onAbove", out var o) ? 
            (o is JsonElement je1 ? je1.GetDouble() : Convert.ToDouble(o)) : 26.0;
        double offBelow = parameters.TryGetValue("offBelow", out var f) ? 
            (f is JsonElement je2 ? je2.GetDouble() : Convert.ToDouble(f)) : 24.0;

        readings.TryGetValue("Temperature", out double temp);

        var result = new List<ActuatorCommand>();

        if (temp >= onAbove)
            result.Add(new ActuatorCommand("Fan", "On"));
        else if (temp <= offBelow)
            result.Add(new ActuatorCommand("Fan", "Off"));

        return Task.FromResult<IEnumerable<ActuatorCommand>>(result);
    }
}
