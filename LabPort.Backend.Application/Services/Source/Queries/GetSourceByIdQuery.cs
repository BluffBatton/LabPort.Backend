using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Query
{
    public class GetSourceByIdQuery : IRequest<SourceDto>
    {
        public Guid SourceId { get; set; }
        public GetSourceByIdQuery(Guid sourceId)
        {
            SourceId = sourceId;
        }
    }

    public class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetSourceByIdQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SourceDto> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = await _context.Sources
                .AsNoTracking()
                .Where(s => s.Id == request.SourceId)
                .ProjectTo<SourceDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if(source == null)
            {
                throw new Exception($"Source with Id {request.SourceId} was not found");
            }
            
            return source;
        }
    }
}
