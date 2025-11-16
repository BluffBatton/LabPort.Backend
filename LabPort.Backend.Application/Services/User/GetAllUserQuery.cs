using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;

namespace LabPort.Backend.Application.Services.User
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
        public GetAllUserQuery() { }
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
