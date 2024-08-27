using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.AppUser.AssignRoleToUser;
using OHS_program_api.Application.Features.Commands.AppUser.CreateUser;
using OHS_program_api.Application.Features.Commands.AppUser.RemoveUser;
using OHS_program_api.Application.Features.Commands.AppUser.UpdatePassword;
using OHS_program_api.Application.Features.Commands.AppUser.UpdateUser;
using OHS_program_api.Application.Features.Queries.AppUser.GetAllUsers;
using OHS_program_api.Application.Features.Queries.AppUser.GetRolesToUser;

namespace OHS_program_api.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IMailService _mailService;
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create User", Menu = "Users")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update User", Menu = "Users")]
        public async Task<IActionResult> UpdateUser (UpdateUserCommandRequest updateUserCommandRequest)
        {
            UpdateUserCommandResponse response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update User", Menu = "Users")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Remove User", Menu = "Users")]
        public async Task<IActionResult> RemoveUser([FromRoute] RemoveUserCommandRequest removeUserCommandRequest)
        {
            RemoveUserCommandResponse response = await _mediator.Send(removeUserCommandRequest);
            return Ok(response);
        }
    }
}