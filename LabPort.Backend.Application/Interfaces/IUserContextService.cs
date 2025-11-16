using System.Security.Claims;

namespace LabPort.Backend.Application.Interfaces
{
    public interface IUserContextService
    {
        Guid? GetCurrentUserId();
        ClaimsPrincipal? GetCurrentUser();
        bool IsAuthenticated();
    }
}
