using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Queries
{
    public class GetAllTestsQuery : IRequest<List<TestDto>>
    {
    }

    public class GetAllTestsQueryHandler : IRequestHandler<GetAllTestsQuery, List<TestDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetAllTestsQueryHandler(
            ILabPortDbContext context, 
            IMapper mapper, 
            IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<List<TestDto>> Handle(GetAllTestsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            return await _context.Tests
                .AsNoTracking()
                .Where(t => t.Sample.Container.UserId == userId)
                .ProjectTo<TestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
