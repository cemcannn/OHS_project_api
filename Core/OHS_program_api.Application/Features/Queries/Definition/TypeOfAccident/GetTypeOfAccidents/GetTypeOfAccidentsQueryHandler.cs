using MediatR;
using OHS_program_api.Application.Features.Queries.Personnel.GetPersonnels;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccident
{
    public class GetTypeOfAccidentsQueryHandler : IRequestHandler<GetTypeOfAccidentsQueryRequest, GetTypeOfAccidentsQueryResponse>
    {
        readonly ITypeOfAccidentReadRepository _typeOfAccidentReadRepository;

        public GetTypeOfAccidentsQueryHandler(ITypeOfAccidentReadRepository typeOfAccidentReadRepository)
        {
            _typeOfAccidentReadRepository = typeOfAccidentReadRepository;
        }

        public async Task<GetTypeOfAccidentsQueryResponse> Handle(GetTypeOfAccidentsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _typeOfAccidentReadRepository.GetAll(false).Count();
            var typeOfAccident = _typeOfAccidentReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size)

                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = typeOfAccident,
                TotalCount = totalCount
            };
        }
    }
}
