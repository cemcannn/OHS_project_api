
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OHS_program_api.Application.Abstractions.Services.Configurations;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.DTOs.Configuration;
using OHS_program_api.Application.Enums;
using System.Reflection;


namespace OHS_program_api.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                foreach (var action in actions)
                {
                    var attributes = action.GetCustomAttributes(true);

                    var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                    if (authorizeDefinitionAttribute == null)
                        continue;

                    var menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                    if (menu == null)
                    {
                        menu = new Menu { Name = authorizeDefinitionAttribute.Menu };
                        menus.Add(menu);
                    }

                    Application.DTOs.Configuration.Action _action = new()
                    {
                        ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                        Definition = authorizeDefinitionAttribute.Definition
                    };

                    var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                    _action.HttpType = httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get;

                    _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                    menu.Actions.Add(_action);
                }
            }
            return menus;
        }
    }
}