using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Queries
{
    public class GetAllSamplesQuery : IRequest<List<SampleDto>>
    {

    }

    public class GetAllSamplesQueryHandler : IRequestHandler<GetAllSamplesQuery, List<SampleDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetAllSamplesQueryHandler(
            ILabPortDbContext context, 
            IMapper mapper, 
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<List<SampleDto>> Handle(GetAllSamplesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            return await _context.Samples
                .AsNoTracking()
                .Where(s => s.Container.UserId == userId.Value) 
                .ProjectTo<SampleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}