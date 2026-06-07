using LabPort.Backend.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabPort.Backend.API.Common
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IUserContextService _userContextService;
        //private IPdfReportService _pdfReportService;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IUserContextService UserContextService =>
            _userContextService ??= HttpContext.RequestServices.GetService<IUserContextService>();

        //protected IPdfReportService PdfReportService =>
        //     _pdfReportService ??= HttpContext.RequestServices.GetService<IPdfReportService>();

        protected Guid UserId => UserContextService.GetCurrentUserId() ?? Guid.Empty;
    }
}
