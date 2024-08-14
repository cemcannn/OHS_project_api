using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.Limb.CreateLimb;
using OHS_program_api.Application.Features.Commands.Definition.Limb.RemoveLimb;
using OHS_program_api.Application.Features.Commands.Definition.Limb.UpdateLimb;
using OHS_program_api.Application.Features.Queries.Definition.Limb.GetLimbById;
using OHS_program_api.Application.Features.Queries.Definition.Limb.GetLimbs;


namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimbsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<LimbsController> _logger;

        public LimbsController(IMediator mediator, ILogger<LimbsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Limb By Id", Menu = "Limbs")]
        public async Task<IActionResult> GetLimb([FromRoute] GetLimbByIdQueryRequest getLimbByIdQueryRequest)
        {
            GetLimbByIdQueryResponse response = await _mediator.Send(getLimbByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Limbs", Menu = "Limbs")]
        public async Task<IActionResult> GetLimbs([FromQuery] GetLimbsQueryRequest GetLimbsQueryRequest)
        {
            GetLimbsQueryResponse response = await _mediator.Send(GetLimbsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Limb", Menu = "Limbs")]
        public async Task<IActionResult> CreateLimb([FromBody] CreateLimbCommandRequest createLimbCommandRequest)
        {
            CreateLimbCommandResponse response = await _mediator.Send(createLimbCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Limb", Menu = "Limbs")]
        public async Task<IActionResult> UpdateLimb([FromBody] UpdateLimbCommandRequest updateLimbCommandRequest)
        {
            UpdateLimbCommandResponse response = await _mediator.Send(updateLimbCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Limb", Menu = "Limbs")]
        public async Task<IActionResult> DeleteLimb([FromRoute] RemoveLimbCommandRequest removeLimbCommandRequest)
        {
            RemoveLimbCommandResponse response = await _mediator.Send(removeLimbCommandRequest);
            return Ok(response);
        }
    }
}
