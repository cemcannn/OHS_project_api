using MediatR;
using OHS_program_api.Application.Features.Queries.Safety.GetAccidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccidentById
{
    public class GetTypeOfAccidentByIdQueryRequest : IRequest<GetTypeOfAccidentByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
