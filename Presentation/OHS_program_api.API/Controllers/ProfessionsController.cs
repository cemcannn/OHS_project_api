using MediatR;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Definition.Profession.CreateProfession;
using OHS_program_api.Application.Features.Commands.Definition.Profession.RemoveProfession;
using OHS_program_api.Application.Features.Commands.Definition.Profession.UpdateProfession;
using OHS_program_api.Application.Features.Queries.Definition.Profession.GetProfessionById;
using OHS_program_api.Application.Features.Queries.Definition.Profession.GetProfessions;


namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<ProfessionsController> _logger;

        public ProfessionsController(IMediator mediator, ILogger<ProfessionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Profession By Id", Menu = "Professions")]
        public async Task<IActionResult> GetProfession([FromRoute] GetProfessionByIdQueryRequest getProfessionByIdQueryRequest)
        {
            GetProfessionByIdQueryResponse response = await _mediator.Send(getProfessionByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Professions", Menu = "Professions")]
        public async Task<IActionResult> GetProfessions([FromQuery] GetProfessionsQueryRequest GetProfessionsQueryRequest)
        {
            GetProfessionsQueryResponse response = await _mediator.Send(GetProfessionsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Profession", Menu = "Professions")]
        public async Task<IActionResult> CreateProfession([FromBody] CreateProfessionCommandRequest createProfessionCommandRequest)
        {
            CreateProfessionCommandResponse response = await _mediator.Send(createProfessionCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Profession", Menu = "Professions")]
        public async Task<IActionResult> UpdateProfession([FromBody] UpdateProfessionCommandRequest updateProfessionCommandRequest)
        {
            UpdateProfessionCommandResponse response = await _mediator.Send(updateProfessionCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Profession", Menu = "Professions")]
        public async Task<IActionResult> DeleteProfession([FromRoute] RemoveProfessionCommandRequest removeProfessionCommandRequest)
        {
            RemoveProfessionCommandResponse response = await _mediator.Send(removeProfessionCommandRequest);
            return Ok(response);
        }
    }
}
