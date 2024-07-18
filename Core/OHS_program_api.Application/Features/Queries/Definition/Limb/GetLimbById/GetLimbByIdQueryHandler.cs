using MediatR;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Limb.GetLimbById
{
    public class GetLimbByIdQueryHandler : IRequestHandler<GetLimbByIdQueryRequest, GetLimbByIdQueryResponse>
    {
        readonly ILimbReadRepository _limbReadRepository;

        public GetLimbByIdQueryHandler(ILimbReadRepository limbReadRepository)
        {
            _limbReadRepository = limbReadRepository;
        }

        public async Task<GetLimbByIdQueryResponse> Handle(GetLimbByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the Limb by Id
            var limb = await _limbReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = limb.Id.ToString(),
                Name = limb.Name
            };
        }
    }
}
