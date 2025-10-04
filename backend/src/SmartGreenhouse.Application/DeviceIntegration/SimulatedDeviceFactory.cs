using SmartGreenhouse.Domain.Enums;

public sealed class SimulatedDeviceFactory : IDeviceIntegrationFactory
{
    public ISensorReader CreateSensorReader() => new SimulatedSensorReader();
    public IActuatorController CreateActuatorController() => new SimulatedActuatorController();

    private sealed class SimulatedSensorReader : ISensorReader
    {
        private static readonly Random _rng = new();
        public Task<double> ReadAsync(int deviceId, SensorTypeEnum sensorType, CancellationToken ct = default)
            => Task.FromResult(sensorType switch
            {
                SensorTypeEnum.Temperature => 20 + _rng.NextDouble() * 6,
                SensorTypeEnum.Humidity => 40 + _rng.NextDouble() * 30,
                SensorTypeEnum.Light => 300 + _rng.NextDouble() * 600,
                SensorTypeEnum.SoilMoisture => 20 + _rng.NextDouble() * 40,
                _ => 0
            });

        public string UnitFor(SensorTypeEnum sensorType) => sensorType switch
        {
            SensorTypeEnum.Temperature => "°C",
            SensorTypeEnum.Humidity => "%",
            SensorTypeEnum.Light => "lux",
            SensorTypeEnum.SoilMoisture => "%",
            _ => ""
        };
    }

    private sealed class SimulatedActuatorController : IActuatorController
    {
        public Task SetStateAsync(int deviceId, string actuatorName, bool on, CancellationToken ct = default)
        {
            Console.WriteLine($"[SIM] Device {deviceId}: {actuatorName} => {(on ? "ON" : "OFF")}");
            return Task.CompletedTask;
        }
    }
}
