using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.CreateDirectorate;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.RemoveDirectorate;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.UpdateDirectorate;
using OHS_program_api.Application.Features.Queries.Definition.Directorate.GetDirectorateById;
using OHS_program_api.Application.Features.Queries.Definition.Directorate.GetDirectorates;


namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoratesController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<DirectoratesController> _logger;

        public DirectoratesController(IMediator mediator, ILogger<DirectoratesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Directorate By Id", Menu = "Directorates")]
        public async Task<IActionResult> GetDirectorate([FromRoute] GetDirectorateByIdQueryRequest getDirectorateByIdQueryRequest)
        {
            GetDirectorateByIdQueryResponse response = await _mediator.Send(getDirectorateByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Directorates", Menu = "Directorates")]
        public async Task<IActionResult> GetDirectorates([FromQuery] GetDirectoratesQueryRequest GetDirectoratesQueryRequest)
        {
            GetDirectoratesQueryResponse response = await _mediator.Send(GetDirectoratesQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Directorate", Menu = "Directorates")]
        public async Task<IActionResult> CreateDirectorate([FromBody] CreateDirectorateCommandRequest createDirectorateCommandRequest)
        {
            CreateDirectorateCommandResponse response = await _mediator.Send(createDirectorateCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Directorate", Menu = "Directorates")]
        public async Task<IActionResult> UpdateDirectorate([FromBody] UpdateDirectorateCommandRequest updateDirectorateCommandRequest)
        {
            UpdateDirectorateCommandResponse response = await _mediator.Send(updateDirectorateCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Directorate", Menu = "Directorates")]
        public async Task<IActionResult> DeleteDirectorate([FromRoute] RemoveDirectorateCommandRequest removeDirectorateCommandRequest)
        {
            RemoveDirectorateCommandResponse response = await _mediator.Send(removeDirectorateCommandRequest);
            return Ok(response);
        }
    }
}
