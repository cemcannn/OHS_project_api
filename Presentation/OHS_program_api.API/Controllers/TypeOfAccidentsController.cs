using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.CreateTypeOfAccident;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.RemoveTypeOfAccident;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident;
using OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccident;
using OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccidentById;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfAccidentsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<TypeOfAccidentsController> _logger;

        public TypeOfAccidentsController(IMediator mediator, ILogger<TypeOfAccidentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Type Of Accident By Id", Menu = "TypeOfAccidents")]
        public async Task<IActionResult> GetTypeOfAccident([FromRoute] GetTypeOfAccidentByIdQueryRequest getTypeOfAccidentByIdQueryRequest)
        {
            GetTypeOfAccidentByIdQueryResponse response = await _mediator.Send(getTypeOfAccidentByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Type Of Accidents", Menu = "TypeOfAccidents")]
        public async Task<IActionResult> GetTypeOfAccidents([FromQuery] GetTypeOfAccidentsQueryRequest GetTypeOfAccidentsQueryRequest)
        {
            GetTypeOfAccidentsQueryResponse response = await _mediator.Send(GetTypeOfAccidentsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Type Of Accident", Menu = "TypeOfAccidents")]
        public async Task<IActionResult> CreateTypeOfAccident([FromBody] CreateTypeOfAccidentCommandRequest createTypeOfAccidentCommandRequest)
        {
            CreateTypeOfAccidentCommandResponse response = await _mediator.Send(createTypeOfAccidentCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Type Of Accident", Menu = "TypeOfAccidents")]
        public async Task<IActionResult> UpdateTypeOfAccident([FromBody] UpdateTypeOfAccidentCommandRequest updateTypeOfAccidentCommandRequest)
        {
            UpdateTypeOfAccidentCommandResponse response = await _mediator.Send(updateTypeOfAccidentCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Type Of Accident", Menu = "TypeOfAccidents")]
        public async Task<IActionResult> DeleteTypeOfAccident([FromRoute] RemoveTypeOfAccidentCommandRequest removeTypeOfAccidentCommandRequest)
        {
            RemoveTypeOfAccidentCommandResponse response = await _mediator.Send(removeTypeOfAccidentCommandRequest);
            return Ok(response);
        }
    }
}
