using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartGreenhouse.Application.Control;

namespace SmartGreenhouse.Application.Control;

public class MoistureTopUpStrategy : IControlStrategy
{
    public Task<IEnumerable<ActuatorCommand>> EvaluateAsync(ControlContext context)
    {
        var readings = context.Readings ?? new Dictionary<string, double>();
        var parameters = context.Parameters ?? new Dictionary<string, object>();

        double minMoisture = parameters.TryGetValue("minMoisture", out var m) ? Convert.ToDouble(m) : 30.0;

        readings.TryGetValue("SoilMoisture", out double moisture);

        var result = new List<ActuatorCommand>();

        if (moisture < minMoisture)
            result.Add(new ActuatorCommand("Pump", "On"));
        else
            result.Add(new ActuatorCommand("Pump", "Off"));

        return Task.FromResult<IEnumerable<ActuatorCommand>>(result);
    }
}
