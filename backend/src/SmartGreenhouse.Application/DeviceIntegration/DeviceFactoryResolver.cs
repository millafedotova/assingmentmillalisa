using Microsoft.Extensions.DependencyInjection;
using SmartGreenhouse.Domain.Enums;

public interface IDeviceFactoryResolver
{
    IDeviceIntegrationFactory Resolve(Device device);
}

public class DeviceFactoryResolver : IDeviceFactoryResolver
{
    private readonly IServiceProvider _sp;
    public DeviceFactoryResolver(IServiceProvider sp) => _sp = sp;

    public IDeviceIntegrationFactory Resolve(Device device) =>
        device.DeviceType switch
        {
            DeviceTypeEnum.Simulated => _sp.GetRequiredService<SimulatedDeviceFactory>(),
            // DeviceTypeEnum.MqttEdge => _sp.GetRequiredService<MqttDeviceFactory>(),
            _ => throw new NotSupportedException()
        };
}
