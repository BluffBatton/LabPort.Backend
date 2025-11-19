using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Query
{
    public class GetSourceTypeQuery : IRequest<List<SourceTypeDto>>
    {
    }

    public class GetSourceTypeQueryHandler : IRequestHandler<GetSourceTypeQuery, List<SourceTypeDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        public GetSourceTypeQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SourceTypeDto>> Handle(GetSourceTypeQuery request, CancellationToken cancellationToken)
        {
            return await _context.SourceTypes
                .AsNoTracking()
                .ProjectTo<SourceTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}