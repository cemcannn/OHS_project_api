using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.CustomAttributes;
using System.Reflection;

namespace OHS_program_api.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        private const string ObserverRoleName = "Observer";
        private const string SuperAdminRoleName = "SuperAdmin";
        private const string AdminRoleName = "Admin";

        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var name = context.HttpContext.User.Identity?.Name;
            if (!string.IsNullOrEmpty(name))
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor?.MethodInfo == null)
                {
                    await next();
                    return;
                }

                var attribute = descriptor.MethodInfo.GetCustomAttribute<AuthorizeDefinitionAttribute>();
                if (attribute == null)
                {
                    await next();
                    return;
                }

                var httpAttribute = descriptor.MethodInfo.GetCustomAttribute<HttpMethodAttribute>();
                var httpMethod = httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get;
                var definition = attribute.Definition?.Replace(" ", string.Empty) ?? string.Empty;
                var code = $"{httpMethod}.{attribute.ActionType}.{definition}";

                var hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);

                var userRoles = await _userService.GetRolesToUserAsync(name);
                var isSuperAdmin = userRoles.Any(r => string.Equals(r, SuperAdminRoleName, StringComparison.OrdinalIgnoreCase));
                var isAdmin = userRoles.Any(r => string.Equals(r, AdminRoleName, StringComparison.OrdinalIgnoreCase));
                var isObserverOnly = userRoles.Any(r => string.Equals(r, ObserverRoleName, StringComparison.OrdinalIgnoreCase)) && !isSuperAdmin && !isAdmin;

                if (isAdmin)
                {
                    await next();
                    return;
                }

                if (isObserverOnly)
                {
                    if (attribute.ActionType != Application.Enums.ActionType.Reading)
                        context.Result = new UnauthorizedResult();
                    else
                        await next();

                    return;
                }

                if (!hasRole)
                    context.Result = new UnauthorizedResult();
                else
                    await next();
            }
            else
                await next();
        }
    }
}