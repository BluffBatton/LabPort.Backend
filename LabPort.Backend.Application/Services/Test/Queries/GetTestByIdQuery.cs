using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LabPort.Backend.Application.Services.Test.Queries
{
    public class GetTestByIdQuery : IRequest<TestDto>
    {
        public Guid TestId { get; set; }

        public GetTestByIdQuery(Guid testId)
        {
            TestId = testId;
        }
    }

    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, TestDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetTestByIdQueryHandler(
            ILabPortDbContext context, 
            IMapper mapper, 
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<TestDto> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var test = await _context.Tests
                .AsNoTracking()
                .Where(t => t.Id == request.TestId && t.Sample.Container.UserId == userId)
                .ProjectTo<TestDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if(test == null)
            {
                throw new Exception($"Test with Id {request.TestId} not found");
            }

            return test;
        }
    }
}