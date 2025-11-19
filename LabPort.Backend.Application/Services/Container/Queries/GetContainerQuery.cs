using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Container.Queries
{
    public class GetContainerQuery : IRequest<ContainerDto> { }

    public class GetContainerQueryHandler : IRequestHandler<GetContainerQuery, ContainerDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public GetContainerQueryHandler(
            ILabPortDbContext context, 
            IUserContextService userContextService,
            IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<ContainerDto> Handle(GetContainerQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }

            var container = await _context.Containers.FirstOrDefaultAsync(x => x.UserId == userId.Value, cancellationToken);

            return _mapper.Map<ContainerDto>(container);
        }
    }
}
