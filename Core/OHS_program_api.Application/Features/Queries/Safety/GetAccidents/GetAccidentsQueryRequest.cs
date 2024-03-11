using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Safety.GetAccidents
{
    public class GetAccidentsQueryRequest : IRequest<GetAccidentsQueryResponse>
    {
        public string Id { get; set; }
    }
}
