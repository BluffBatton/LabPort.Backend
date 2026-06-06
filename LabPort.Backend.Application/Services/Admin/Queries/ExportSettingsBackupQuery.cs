using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Admin.Queries
{
    public class ExportSettingsBackupQuery : IRequest<SettingsBackupDto>
    {
    }

    public class ExportSettingsBackupQueryHandler : IRequestHandler<ExportSettingsBackupQuery, SettingsBackupDto>
    {
        private readonly ILabPortDbContext _context;

        public ExportSettingsBackupQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<SettingsBackupDto> Handle(ExportSettingsBackupQuery request, CancellationToken cancellationToken)
        {
            return new SettingsBackupDto
            {
                ExportedAt = DateTime.UtcNow,
                SourceTypes = await _context.SourceTypes
                    .AsNoTracking()
                    .OrderBy(st => st.Name)
                    .Select(st => new SettingsBackupSourceTypeDto
                    {
                        Name = st.Name
                    })
                    .ToListAsync(cancellationToken),
                TestTypes = await _context.TestTypes
                    .AsNoTracking()
                    .OrderBy(tt => tt.Name)
                    .Select(tt => new SettingsBackupTestTypeDto
                    {
                        Name = tt.Name,
                        ReferenceMin = tt.ReferenceMin,
                        ReferenceMax = tt.ReferenceMax,
                        Unit = tt.Unit
                    })
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
