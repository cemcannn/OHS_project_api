using MediatR;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.CreateActualDailyWage;
using OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.DeleteActualDailyWage;
using OHS_program_api.Application.Features.Commands.Safety.ActualDailyWage.UpdateActualDailyWage;
using OHS_program_api.Application.Features.Queries.Safety.ActualDailyWage.GetActualDailyWages;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActualDailyWagesController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<ActualDailyWagesController> _logger;

        public ActualDailyWagesController(IMediator mediator, ILogger<ActualDailyWagesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[HttpGet("{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get ActualDailyWage By Id", Menu = "ActualDailyWages")]
        //public async Task<IActionResult> GetActualDailyWage([FromRoute] GetActualDailyWageByIdQueryRequest getActualDailyWageByIdQueryRequest)
        //{
        //    GetActualDailyWageByIdQueryResponse response = await _mediator.Send(getActualDailyWageByIdQueryRequest);
        //    return Ok(response);
        //}

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All ActualDailyWages", Menu = "ActualDailyWages")]
        public async Task<IActionResult> GetActualDailyWages([FromQuery] GetActualDailyWagesQueryRequest GetActualDailyWagesQueryRequest)
        {
            GetActualDailyWagesQueryResponse response = await _mediator.Send(GetActualDailyWagesQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create ActualDailyWage", Menu = "ActualDailyWages")]
        public async Task<IActionResult> CreateActualDailyWages([FromBody] CreateActualDailyWageCommandRequest createActualDailyWageCommandRequest)
        {
            CreateActualDailyWageCommandResponse response = await _mediator.Send(createActualDailyWageCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update ActualDailyWage", Menu = "ActualDailyWages")]
        public async Task<IActionResult> UpdateActualDailyWages([FromBody] UpdateActualDailyWageCommandRequest updateActualDailyWageCommandRequest)
        {
            UpdateActualDailyWageCommandResponse response = await _mediator.Send(updateActualDailyWageCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete ActualDailyWage", Menu = "ActualDailyWages")]
        public async Task<IActionResult> DeleteActualDailyWages([FromRoute] DeleteActualDailyWageCommandRequest deleteActualDailyWageCommandRequest)
        {
            DeleteActualDailyWageCommandResponse response = await _mediator.Send(deleteActualDailyWageCommandRequest);
            return Ok(response);
        }
    }
}
