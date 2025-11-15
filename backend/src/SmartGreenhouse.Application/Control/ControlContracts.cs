using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartGreenhouse.Application.Control
{
    public record ActuatorCommand(string ActuatorName, string Action)
    {
        public object? Target { get; internal set; }
    }

    public class ControlContext
    {
        public int DeviceId { get; set; }
        public Dictionary<string, double> Readings { get; set; } = new();

        public Dictionary<string, object>? Parameters { get; set; }
    }

    public interface IControlStrategy
    {
        Task<IEnumerable<ActuatorCommand>> EvaluateAsync(ControlContext context);
    }

    public interface IActuatorAdapter
    {
        Task ApplyAsync(int deviceId, IReadOnlyList<ActuatorCommand> commands, CancellationToken ct = default);
    }

    public interface INotificationAdapter
    {
        Task NotifyAsync(int deviceId, string message, CancellationToken ct = default);
    }
}
