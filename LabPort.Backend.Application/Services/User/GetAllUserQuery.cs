using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.User
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
        public GetAllUserQuery() { }
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly ILabPortDbContext _context;

        public GetAllUserQueryHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.Select(u => new UserDto 
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = (LabPort.Backend.Contracts.DTOs.Enums.Role)u.Role, // explicit cast to DTO Role enum
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                LastLoginAt = u.LastLoginAt,
            }).ToListAsync();
        }
    }
}