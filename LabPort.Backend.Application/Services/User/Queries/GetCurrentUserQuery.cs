using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.User.Queries
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {

    }

    public class GetUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public GetUserQueryHandler(ILabPortDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();

            if(userId == null)
            {
                throw new Exception("User is not authenticated");
            }

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId.Value, cancellationToken);

            if(user == null)
            {
                throw new Exception($"TROUBLE: User with id {userId} was not found in database.");
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
