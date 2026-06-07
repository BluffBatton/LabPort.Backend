using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sample.Queries
{
    public class SearchSamplesQuery : IRequest<List<SampleDto>>
    {
        public string? Name { get; set; }
        public Guid? SourceId { get; set; }
        public string? SourceTypeName { get; set; }
        public Guid? ContainerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class SearchSamplesQueryHandler : IRequestHandler<SearchSamplesQuery, List<SampleDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public SearchSamplesQueryHandler(
            ILabPortDbContext context,
            IMapper mapper,
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<List<SampleDto>> Handle(SearchSamplesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var query = _context.Samples
                .AsNoTracking()
                .Where(s => s.Container.UserId == userId.Value)
                .Include(s => s.Source).ThenInclude(st => st.SourceType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(s => s.Name.Contains(request.Name));

            if (request.SourceId.HasValue)
                query = query.Where(s => s.SourceId == request.SourceId.Value);

            if (!string.IsNullOrWhiteSpace(request.SourceTypeName))
                query = query.Where(s => s.Source.SourceType.Name == request.SourceTypeName);

            if (request.ContainerId.HasValue)
                query = query.Where(s => s.ContainerId == request.ContainerId.Value);

            if (request.DateFrom.HasValue)
                query = query.Where(s => s.CollectedAt >= request.DateFrom.Value);

            if (request.DateTo.HasValue)
                query = query.Where(s => s.CollectedAt <= request.DateTo.Value);

            return await query
                .ProjectTo<SampleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }

}
