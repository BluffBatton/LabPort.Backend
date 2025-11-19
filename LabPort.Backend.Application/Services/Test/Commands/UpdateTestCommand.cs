using LabPort.Backend.Application.Interfaces;
using LabPort.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LabPort.Backend.Application.Services.Test.Commands
{
    public class UpdateTestCommand : IRequest
    {
        public Guid TestId { get; set; }
        public TestUpdatedDto Dto { get; set; }

        public UpdateTestCommand(Guid testId, TestUpdatedDto dto) 
        {
            TestId = testId;
            Dto = dto;
        }
    }

    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
    {
        private readonly ILabPortDbContext _context;
        private readonly IUserContextService _userContextService;

        public UpdateTestCommandHandler(
            ILabPortDbContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContextService.GetCurrentUserId();
            if (userId == null)
            {
                throw new Exception($"User is not authenticated");
            }

            var test = await _context.Tests
                .Include(t => t.Sample).ThenInclude(s => s.Container)
                .FirstOrDefaultAsync(t => t.Id == request.TestId, cancellationToken);

            if (test is null || test.Sample.Container.UserId != userId)
            {
                throw new Exception("Test not found or access denied");
            }

            var dto = request.Dto;

            if (dto.TestStatus.HasValue)
            {
                test.TestStatus = (Domain.Enums.TestStatus)dto.TestStatus.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.Comment))
            {
                test.Comment = dto.Comment;
            }

            if (dto.TestTypeId.HasValue)
            {
                test.TestTypeId = dto.TestTypeId.Value;
            }

            test.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}