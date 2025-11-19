using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.TestResult.Queries
{
    public class GetAllTestResultsQuery : IRequest<List<TestResultDto>>
    {
    }

    public class GetAllTestResultsQueryHandler : IRequestHandler<GetAllTestResultsQuery, List<TestResultDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTestResultsQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TestResultDto>> Handle(GetAllTestResultsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TestResults
                .AsNoTracking()
                .ProjectTo<TestResultDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
