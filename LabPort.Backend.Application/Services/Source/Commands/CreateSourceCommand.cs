using AutoMapper;
using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.CreateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Source.Command
{
    public class CreateSourceCommand : IRequest
    {
        public SourceCreateDto Dto { get; set; }

        public CreateSourceCommand(SourceCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IMapper _mapper;

        public CreateSourceCommandHandler(ILabPortDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(CreateSourceCommand request, CancellationToken cancellationToken)
        {

            var sourceExists = await _context.Sources.AnyAsync(s => s.Name == request.Dto.Name, cancellationToken);

            if (sourceExists)
            {
                throw new InvalidOperationException($"Source with name {request.Dto.Name} already exists");
            }

            var source = _mapper.Map<Domain.Entities.Source>(sourceExists);
            source.CreatedAt = DateTime.UtcNow;

            await _context.Sources.AddAsync(source, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}