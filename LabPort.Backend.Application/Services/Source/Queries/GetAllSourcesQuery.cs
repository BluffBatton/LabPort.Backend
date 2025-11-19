using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Query
{
    public record GetAllSourcesQuery : IRequest<List<SourceDto>>
    {
    }

    public class GetAllSourcesQueryHandler : IRequestHandler<GetAllSourcesQuery, List<SourceDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSourcesQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SourceDto>> Handle(GetAllSourcesQuery request, CancellationToken cancellationToken)
        {
            var sources = await _context.Sources
                .AsNoTracking()
                .ProjectTo<SourceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return sources;
        }
    }
}
