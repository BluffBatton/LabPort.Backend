using LabPort.Backend.Domain.Entities;

namespace LabPort.Backend.Application.Interfaces
{
    public interface IAlertService
    {
        Task CheckAndCreateAlertsAsync(Container container, SensorReading reading, CancellationToken ct);
    }
}
