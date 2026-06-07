namespace LabPort.Backend.Application.Interfaces
{
    public interface IMqttService
    {
        Task PublishAsync(string topic, string payload);
    }
}
