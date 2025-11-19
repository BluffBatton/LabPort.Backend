using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using LabPort.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Queries
{
    public class GetTestTypeQuery : IRequest<List<TestTypeDto>>
    {
    }

    public class GetTestTypeQueryHandler : IRequestHandler<GetTestTypeQuery, List<TestTypeDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetTestTypeQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TestTypeDto>> Handle(GetTestTypeQuery request, CancellationToken cancellationToken)
        {
            return await _context.TestTypes
                .AsNoTracking()
                .ProjectTo<TestTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
