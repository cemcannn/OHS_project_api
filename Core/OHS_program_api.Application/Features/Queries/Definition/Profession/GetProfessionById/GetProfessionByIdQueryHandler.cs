using MediatR;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Profession.GetProfessionById
{
    public class GetProfessionByIdQueryHandler : IRequestHandler<GetProfessionByIdQueryRequest, GetProfessionByIdQueryResponse>
    {
        readonly IProfessionReadRepository _professionReadRepository;

        public GetProfessionByIdQueryHandler(IProfessionReadRepository professionReadRepository)
        {
            _professionReadRepository = professionReadRepository;
        }

        public async Task<GetProfessionByIdQueryResponse> Handle(GetProfessionByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the Profession by Id
            var profession = await _professionReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = profession.Id.ToString(),
                Name = profession.Name
            };
        }
    }
}
