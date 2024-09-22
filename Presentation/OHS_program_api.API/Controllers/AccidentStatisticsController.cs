using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Consts;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.CreateAccidentStatistic;
using OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.DeleteAccidentStatistic;
using OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.UpdateAccidentStatistic;
using OHS_program_api.Application.Features.Queries.Safety.AccidentStatistic.GetAccidentStatistics;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Admin")]
    public class AccidentStatisticsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<AccidentStatisticsController> _logger;

        public AccidentStatisticsController(IMediator mediator, ILogger<AccidentStatisticsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[HttpGet("{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get AccidentStatistic By Id", Menu = AuthorizeDefinitionConstants.AccidentStatistics)]
        //public async Task<IActionResult> GetAccidentStatistic([FromRoute] GetAccidentStatisticByIdQueryRequest getAccidentStatisticByIdQueryRequest)
        //{
        //    GetAccidentStatisticByIdQueryResponse response = await _mediator.Send(getAccidentStatisticByIdQueryRequest);
        //    return Ok(response);
        //}

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All AccidentStatistics", Menu = AuthorizeDefinitionConstants.AccidentStatistics)]
        public async Task<IActionResult> GetAccidentStatistics([FromQuery] GetAccidentStatisticsQueryRequest GetAccidentStatisticsQueryRequest)
        {
            GetAccidentStatisticsQueryResponse response = await _mediator.Send(GetAccidentStatisticsQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create AccidentStatistic", Menu = AuthorizeDefinitionConstants.AccidentStatistics)]
        public async Task<IActionResult> CreateAccidentStatistics([FromBody] CreateAccidentStatisticCommandRequest createAccidentStatisticCommandRequest)
        {
            CreateAccidentStatisticCommandResponse response = await _mediator.Send(createAccidentStatisticCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update AccidentStatistic", Menu = AuthorizeDefinitionConstants.AccidentStatistics)]
        public async Task<IActionResult> UpdateAccidentStatistics([FromBody] UpdateAccidentStatisticCommandRequest updateAccidentStatisticCommandRequest)
        {
            UpdateAccidentStatisticCommandResponse response = await _mediator.Send(updateAccidentStatisticCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete AccidentStatistic", Menu = AuthorizeDefinitionConstants.AccidentStatistics)]
        public async Task<IActionResult> DeleteAccidentStatistics([FromRoute] DeleteAccidentStatisticCommandRequest deleteAccidentStatisticCommandRequest)
        {
            DeleteAccidentStatisticCommandResponse response = await _mediator.Send(deleteAccidentStatisticCommandRequest);
            return Ok(response);
        }
    }
}
