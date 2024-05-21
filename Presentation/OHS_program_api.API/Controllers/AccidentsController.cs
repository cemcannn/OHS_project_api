using MediatR;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Safety.Accident.CreateAccident;
using OHS_program_api.Application.Features.Commands.Safety.Accident.DeleteAccident;
using OHS_program_api.Application.Features.Queries.Safety.GetAccidents;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<AccidentsController> _logger;

        public AccidentsController(IMediator mediator, ILogger<AccidentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Accident By Id", Menu = "Accidents")]
        public async Task<IActionResult> GetAccidents([FromRoute] GetAccidentByIdQueryRequest getAccidentByIdQueryRequest)
        {
            GetAccidentByIdQueryResponse response = await _mediator.Send(getAccidentByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Accidents", Menu = "Accidents")]
        public async Task<IActionResult> GetAccidents([FromQuery] GetAccidentsQueryRequest GgtAccidentsQueryRequest)
        {
            GetAccidentsQueryResponse response = await _mediator.Send(GgtAccidentsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Accident", Menu = "Accidents")]
        public async Task<IActionResult> CreateAccident([FromBody] CreateAccidentCommandRequest createAccidentCommandRequest)
        {
            CreateAccidentCommandResponse response = await _mediator.Send(createAccidentCommandRequest);
            return Ok(response);
        }

        //[HttpPut("{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Accident", Menu = "Accidents")]
        //public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        //{
        //    UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
        //    return Ok(response);
        //}

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Accident", Menu = "Accidents")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteAccidentCommandRequest deleteAccidentCommandRequest)
        {
            DeleteAccidentCommandResponse response = await _mediator.Send(deleteAccidentCommandRequest);
            return Ok(response);
        }
    }
}
