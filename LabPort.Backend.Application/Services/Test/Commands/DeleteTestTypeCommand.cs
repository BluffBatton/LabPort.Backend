using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Commands
{
    public class DeleteTestTypeCommand : IRequest
    {
        public Guid TestTypeId { get; set; }

        public DeleteTestTypeCommand(Guid testTypeId)
        {
            TestTypeId = testTypeId;
        }
    }

    public class DeleteTestTypeCommandHandler : IRequestHandler<DeleteTestTypeCommand>
    {
        private readonly ILabPortDbContext _context;

        public DeleteTestTypeCommandHandler(ILabPortDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTestTypeCommand request, CancellationToken cancellationToken)
        {
            var testType = await _context.TestTypes.FirstOrDefaultAsync(tt => tt.Id == request.TestTypeId, cancellationToken);

            if(testType == null)
            {
                throw new KeyNotFoundException($"Test type with Id {request.TestTypeId} was not found");
            }

            bool isUsed = await _context.Tests.AnyAsync(tt => tt.TestTypeId == request.TestTypeId, cancellationToken);

            if (isUsed)
            {
                throw new InvalidOperationException($"Test type with name {testType.Name} is used somewhere, can't be deleted");
            }

            _context.TestTypes.Remove(testType);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
