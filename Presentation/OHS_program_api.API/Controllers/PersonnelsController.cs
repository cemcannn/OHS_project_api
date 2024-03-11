﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Consts;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel;
using OHS_program_api.Application.Features.Commands.Personnel.RemovePersonnel;
using OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels;
using System.Net;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<PersonnelsController> _logger;

        public PersonnelsController(IMediator mediator, ILogger<PersonnelsController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Personnels", Menu = "Personnels")]
        public async Task<IActionResult> GetAllPersonnels([FromQuery] GetPersonnelsQueryRequest getPersonnelsQueryRequest)
        {
            GetPersonnelsQueryResponse response = await _mediator.Send(getPersonnelsQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Personnel", Menu = "Personnels")]
        public async Task<IActionResult> Post(CreatePersonnelCommandRequest createPersonnelCommandRequest)
        {
            CreatePersonnelCommandResponse response = await _mediator.Send(createPersonnelCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Remove Personnel", Menu = "Personnels")]
        public async Task<IActionResult> RemovePersonnel([FromRoute] RemovePersonnelCommandRequest removePersonnelCommandRequest)
        {
            RemovePersonnelCommandResponse response = await _mediator.Send(removePersonnelCommandRequest);
            return Ok(response);
        }

        //[HttpGet("{Id}")]
        //[AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Personnel By Id", Menu = "Personnels")]
        //public async Task<IActionResult> GetPersonnel([FromRoute] GetPersonnelByIdQueryRequest getPersonnelsByIdQueryRequest)
        //{
        //    GetPersonnelByIdQueryResponse response = await _mediator.Send(getPersonnelsByIdQueryRequest);
        //    return Ok(response);
        //}
    }
}
