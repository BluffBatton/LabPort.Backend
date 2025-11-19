using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using MediatR;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class CreateSourceTypeCommand : IRequest
    {
        public SourceTypeCreateDto Dto { get; set; }

        public CreateSourceTypeCommand(SourceTypeCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateSourceTypeCommandHandler : IRequestHandler<CreateSourceTypeCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public CreateSourceTypeCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(CreateSourceTypeCommand request, CancellationToken cancellationToken)
        {
            var sourceType = _mapper.Map<Domain.Entities.SourceType>(request.Dto);
            sourceType.CreatedAt = DateTime.UtcNow;

            await _context.SourceTypes.AddAsync(sourceType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
