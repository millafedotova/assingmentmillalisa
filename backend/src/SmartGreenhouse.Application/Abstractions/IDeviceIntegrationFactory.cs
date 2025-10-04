public interface IDeviceIntegrationFactory
{
    ISensorReader CreateSensorReader();
    IActuatorController CreateActuatorController();
}
