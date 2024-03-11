using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Personnel.GetPersonnelByID
{
    public class GetPersonnelByIdQueryRequest : IRequest<GetPersonnelByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
