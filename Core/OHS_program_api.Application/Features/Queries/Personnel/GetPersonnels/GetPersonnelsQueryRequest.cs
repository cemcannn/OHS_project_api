using MediatR;
using OHS_program_api.Application.Features.Queries.Role.GetRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels
{
    public class GetPersonnelsQueryRequest : IRequest<GetPersonnelsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
