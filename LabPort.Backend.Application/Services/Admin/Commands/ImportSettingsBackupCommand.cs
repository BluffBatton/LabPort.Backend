using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Settings;
using LabPort.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Commands
{
    public class ImportSettingsBackupCommand : IRequest<SettingsBackupImportResultDto>
    {
        public SettingsBackupDto Backup { get; }

        public ImportSettingsBackupCommand(SettingsBackupDto backup)
        {
            Backup = backup;
        }
    }

    public class ImportSettingsBackupCommandHandler : IRequestHandler<ImportSettingsBackupCommand, SettingsBackupImportResultDto>
    {
        private readonly ILabPortDbContext _context;

        public ImportSettingsBackupCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<SettingsBackupImportResultDto> Handle(ImportSettingsBackupCommand request, CancellationToken cancellationToken)
        {
            if (request.Backup == null)
            {
                throw new InvalidOperationException("Settings backup body is required.");
            }

            var result = new SettingsBackupImportResultDto();
            var now = DateTime.UtcNow;

            var existingSourceTypeNames = await _context.SourceTypes
                .Select(st => st.Name)
                .ToListAsync(cancellationToken);

            var sourceTypeNames = new HashSet<string>(existingSourceTypeNames, StringComparer.OrdinalIgnoreCase);
            foreach (var sourceType in request.Backup.SourceTypes ?? Enumerable.Empty<SettingsBackupSourceTypeDto>())
            {
                var name = sourceType.Name?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new InvalidOperationException("Source type name is required.");
                }

                if (!sourceTypeNames.Add(name))
                {
                    result.SkippedSourceTypes++;
                    continue;
                }

                await _context.SourceTypes.AddAsync(new SourceType
                {
                    Name = name,
                    CreatedAt = now
                }, cancellationToken);

                result.CreatedSourceTypes++;
            }

            var existingTestTypeNames = await _context.TestTypes
                .Select(tt => tt.Name)
                .ToListAsync(cancellationToken);

            var testTypeNames = new HashSet<string>(existingTestTypeNames, StringComparer.OrdinalIgnoreCase);
            foreach (var testType in request.Backup.TestTypes ?? Enumerable.Empty<SettingsBackupTestTypeDto>())
            {
                var name = testType.Name?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new InvalidOperationException("Test type name is required.");
                }

                if (!testTypeNames.Add(name))
                {
                    result.SkippedTestTypes++;
                    continue;
                }

                await _context.TestTypes.AddAsync(new TestType
                {
                    Name = name,
                    ReferenceMin = testType.ReferenceMin,
                    ReferenceMax = testType.ReferenceMax,
                    Unit = testType.Unit,
                    CreatedAt = now
                }, cancellationToken);

                result.CreatedTestTypes++;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
