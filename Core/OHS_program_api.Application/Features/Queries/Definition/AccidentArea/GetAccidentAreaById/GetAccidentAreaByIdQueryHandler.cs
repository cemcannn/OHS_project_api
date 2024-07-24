using MediatR;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.AccidentArea.GetAccidentAreaById
{
    public class GetAccidentAreaByIdQueryHandler : IRequestHandler<GetAccidentAreaByIdQueryRequest, GetAccidentAreaByIdQueryResponse>
    {
        readonly IAccidentAreaReadRepository _limbReadRepository;

        public GetAccidentAreaByIdQueryHandler(IAccidentAreaReadRepository limbReadRepository)
        {
            _limbReadRepository = limbReadRepository;
        }

        public async Task<GetAccidentAreaByIdQueryResponse> Handle(GetAccidentAreaByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the AccidentArea by Id
            var limb = await _limbReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = limb.Id.ToString(),
                Name = limb.Name
            };
        }
    }
}
