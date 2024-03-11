using MediatR;
using OHS_program_api.Application.Features.Commands.Role.DeleteRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Personnel.RemovePersonnel
{
    public class RemovePersonnelCommandRequest : IRequest<RemovePersonnelCommandResponse>
    {
        public string Id { get; set; }
    }
}
