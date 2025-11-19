using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Commands
{
    public class UpdateSampleCommand : IRequest
    {
        public Guid Id { get; set; }

        public SampleUpdateDto Sample { get; set; }

        public UpdateSampleCommand(Guid id, SampleUpdateDto sample)
        {
            Id = id;
            Sample = sample;
        }
    }
    public class UpdateSampleCommandHandler : IRequestHandler<UpdateSampleCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public UpdateSampleCommandHandler(
            ILabPortDbContext context, 
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new Exception($"User is not authenticated");
            }

            var sample = await _context.Samples.FirstOrDefaultAsync(s => s.Id == request.Id && s.Container.UserId == userId.Value, cancellationToken);
            if(sample == null)
            {
                throw new Exception($"Sample with Id {request.Id} was not found");
            }

            var dto = request.Sample;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                sample.Name = dto.Name;
            }
            if (dto.SourceId.HasValue)
            {
                sample.SourceId = dto.SourceId.Value;
            }
            if (dto.CollectedAt.HasValue)
            {
                sample.CollectedAt = dto.CollectedAt.Value;
            }

            sample.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
