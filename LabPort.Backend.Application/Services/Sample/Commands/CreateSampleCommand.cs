using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Commands
{
    public class CreateSampleCommand : IRequest
    {
        public SampleCreateDto Sample { get; set; }
        public CreateSampleCommand(SampleCreateDto sample)
        {
            Sample = sample;
        }
    }

    public class CreateSampleCommandHandler : IRequestHandler<CreateSampleCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public CreateSampleCommandHandler(ILabPortDbContext context, 
                                        IMapper mapper, 
                                        IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task Handle(CreateSampleCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var container = await _context.Containers
                .FirstOrDefaultAsync(c => c.UserId == userId.Value, cancellationToken);

            if (container == null)
            {
                throw new Exception($"Container for user '{userId.Value}' was not found");
            }

            var sampleExists = await _context.Samples
                .AnyAsync(s => s.Name == request.Sample.Name && s.ContainerId == container.Id, cancellationToken);

            if (sampleExists)
            {
                throw new Exception($"Sample with name '{request.Sample.Name}' already exists in this container");
            }

            var dto = request.Sample;
            
            var sample = _mapper.Map<Domain.Entities.Sample>(dto);

            sample.ContainerId = container.Id;
            sample.CreatedAt = DateTime.UtcNow;

            await _context.Samples.AddAsync(sample, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
