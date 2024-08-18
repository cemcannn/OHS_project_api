using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Consts;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.AccidentArea.CreateAccidentArea;
using OHS_program_api.Application.Features.Commands.Definition.AccidentArea.RemoveAccidentArea;
using OHS_program_api.Application.Features.Commands.Definition.AccidentArea.UpdateAccidentArea;
using OHS_program_api.Application.Features.Queries.Definition.AccidentArea.GetAccidentAreaById;
using OHS_program_api.Application.Features.Queries.Definition.AccidentArea.GetAccidentAreas;


namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentAreasController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<AccidentAreasController> _logger;

        public AccidentAreasController(IMediator mediator, ILogger<AccidentAreasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get AccidentArea By Id", Menu = AuthorizeDefinitionConstants.AccidentAreas)]
        public async Task<IActionResult> GetAccidentArea([FromRoute] GetAccidentAreaByIdQueryRequest getAccidentAreaByIdQueryRequest)
        {
            GetAccidentAreaByIdQueryResponse response = await _mediator.Send(getAccidentAreaByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All AccidentAreas", Menu = AuthorizeDefinitionConstants.AccidentAreas)]
        public async Task<IActionResult> GetAccidentAreas([FromQuery] GetAccidentAreasQueryRequest GetAccidentAreasQueryRequest)
        {
            GetAccidentAreasQueryResponse response = await _mediator.Send(GetAccidentAreasQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create AccidentArea", Menu = AuthorizeDefinitionConstants.AccidentAreas)]
        public async Task<IActionResult> CreateAccidentArea([FromBody] CreateAccidentAreaCommandRequest createAccidentAreaCommandRequest)
        {
            CreateAccidentAreaCommandResponse response = await _mediator.Send(createAccidentAreaCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update AccidentArea", Menu = AuthorizeDefinitionConstants.AccidentAreas)]
        public async Task<IActionResult> UpdateAccidentArea([FromBody] UpdateAccidentAreaCommandRequest updateAccidentAreaCommandRequest)
        {
            UpdateAccidentAreaCommandResponse response = await _mediator.Send(updateAccidentAreaCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete AccidentArea", Menu = AuthorizeDefinitionConstants.AccidentAreas)]
        public async Task<IActionResult> DeleteAccidentArea([FromRoute] RemoveAccidentAreaCommandRequest removeAccidentAreaCommandRequest)
        {
            RemoveAccidentAreaCommandResponse response = await _mediator.Send(removeAccidentAreaCommandRequest);
            return Ok(response);
        }
    }
}
