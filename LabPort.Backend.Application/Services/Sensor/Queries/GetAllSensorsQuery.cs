using AutoMapper;
using AutoMapper.QueryableExtensions;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Sensor.Queries
{
    public class GetAllSensorsQuery : IRequest<List<SensorDto>>
    {
    }

    public class GetAllSensorsQueryHandler : IRequestHandler<GetAllSensorsQuery, List<SensorDto>>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSensorsQueryHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SensorDto>> Handle(GetAllSensorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Sensors
                .AsNoTracking()
                .ProjectTo<SensorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
