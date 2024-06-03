using MediatR;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;
using OHS_program_api.Application.ViewModels.Safety.Accidents;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OHS_program_api.Application.Features.Queries.Definition.TypeOfAccident.GetTypeOfAccidentById
{
    public class GetTypeOfAccidentByIdQueryHandler : IRequestHandler<GetTypeOfAccidentByIdQueryRequest, GetTypeOfAccidentByIdQueryResponse>
    {
        readonly ITypeOfAccidentReadRepository _typeOfAccidentReadRepository;

        public GetTypeOfAccidentByIdQueryHandler(ITypeOfAccidentReadRepository typeOfAccidentReadRepository)
        {
            _typeOfAccidentReadRepository = typeOfAccidentReadRepository;
        }

        public async Task<GetTypeOfAccidentByIdQueryResponse> Handle(GetTypeOfAccidentByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the TypeOfAccident by Id
            var typeOfAccident = await _typeOfAccidentReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = typeOfAccident.Id.ToString(),
                Name = typeOfAccident.Name
            };
        }
    }
}
