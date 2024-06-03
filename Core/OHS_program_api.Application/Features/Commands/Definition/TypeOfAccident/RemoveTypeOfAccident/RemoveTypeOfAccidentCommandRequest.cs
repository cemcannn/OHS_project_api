using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.CreateTypeOfAccident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.RemoveTypeOfAccident
{
    public class RemoveTypeOfAccidentCommandRequest : IRequest<RemoveTypeOfAccidentCommandResponse>
    {
        public string Id { get; set; }
    }
}
