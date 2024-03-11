using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.DeleteAccident
{
    public class DeleteAccidentCommandRequest : IRequest<DeleteAccidentCommandResponse>
    {
        public string Id { get; set; }
    }
}
