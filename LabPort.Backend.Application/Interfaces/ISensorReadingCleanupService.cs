namespace LabPort.Backend.Application.Interfaces
{
    public interface ISensorReadingCleanupService
    {
        Task CleanupOldReadingsAsync(TimeSpan maxAge, CancellationToken cancellationToken);
    }
}
