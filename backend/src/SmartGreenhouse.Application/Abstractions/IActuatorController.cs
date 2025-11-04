public interface IActuatorController
{
    Task SetStateAsync(int deviceId, string actuatorName, bool on, CancellationToken ct = default);
}
