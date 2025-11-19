using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Queries
{
    public class GetSampleByIdQuery : IRequest<SampleDto>
    {
        public Guid Id { get; set; }

        public GetSampleByIdQuery(Guid id) 
        {
            Id = id;
        }
    }

    public class GetSampleByIdQueryHandler : IRequestHandler<GetSampleByIdQuery, SampleDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetSampleByIdQueryHandler(ILabPortDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<SampleDto> Handle(GetSampleByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var sample = await _context.Samples
                .AsNoTracking()
                .Where(s => s.Id == request.Id &&
                            s.Container.UserId == userId.Value)
                .ProjectTo<SampleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (sample == null) 
            {
                throw new Exception($"Sample with Id {request.Id} was not found");
            }
            return sample;
        }
    }
}
