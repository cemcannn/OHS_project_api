using MediatR;
using OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccident
{
    public class GetTypeOfAccidentsQueryRequest : IRequest<GetTypeOfAccidentsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
