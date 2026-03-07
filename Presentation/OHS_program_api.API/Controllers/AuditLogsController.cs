using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Features.Queries.AuditLog.GetAuditLogs;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuditLogsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuditLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] GetAuditLogsQueryRequest request)
        {
            GetAuditLogsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
