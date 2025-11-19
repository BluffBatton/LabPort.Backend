using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.TestResult.Queries
{
    public class GetTestResultByIdQuery : IRequest<TestResultDto>
    {
        public Guid TestResultId { get; set; }

        public GetTestResultByIdQuery(Guid testResultId)
        {
            TestResultId = testResultId;
        }
    }

    public class GetTestResultByIdQueryHandler : IRequestHandler<GetTestResultByIdQuery, TestResultDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetTestResultByIdQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestResultDto> Handle(GetTestResultByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.TestResults
                .AsNoTracking()
                .Where(r => r.Id == request.TestResultId)
                .ProjectTo<TestResultDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
                throw new Exception($"TestResult with id '{request.TestResultId}' was not found");

            return result;
        }
    }
}
