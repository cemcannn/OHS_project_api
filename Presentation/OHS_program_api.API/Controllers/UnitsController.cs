using MediatR;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.Unit.CreateUnit;
using OHS_program_api.Application.Features.Commands.Definition.Unit.RemoveUnit;
using OHS_program_api.Application.Features.Commands.Definition.Unit.UpdateUnit;
using OHS_program_api.Application.Features.Queries.Definition.Unit.GetUnitById;
using OHS_program_api.Application.Features.Queries.Definition.Unit.GetUnits;


namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<UnitsController> _logger;

        public UnitsController(IMediator mediator, ILogger<UnitsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Unit By Id", Menu = "Units")]
        public async Task<IActionResult> GetUnit([FromRoute] GetUnitByIdQueryRequest getUnitByIdQueryRequest)
        {
            GetUnitByIdQueryResponse response = await _mediator.Send(getUnitByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Units", Menu = "Units")]
        public async Task<IActionResult> GetUnits([FromQuery] GetUnitsQueryRequest GetUnitsQueryRequest)
        {
            GetUnitsQueryResponse response = await _mediator.Send(GetUnitsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Unit", Menu = "Units")]
        public async Task<IActionResult> CreateUnit([FromBody] CreateUnitCommandRequest createUnitCommandRequest)
        {
            CreateUnitCommandResponse response = await _mediator.Send(createUnitCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Unit", Menu = "Units")]
        public async Task<IActionResult> UpdateUnit([FromBody] UpdateUnitCommandRequest updateUnitCommandRequest)
        {
            UpdateUnitCommandResponse response = await _mediator.Send(updateUnitCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Unit", Menu = "Units")]
        public async Task<IActionResult> DeleteUnit([FromRoute] RemoveUnitCommandRequest removeUnitCommandRequest)
        {
            RemoveUnitCommandResponse response = await _mediator.Send(removeUnitCommandRequest);
            return Ok(response);
        }
    }
}
