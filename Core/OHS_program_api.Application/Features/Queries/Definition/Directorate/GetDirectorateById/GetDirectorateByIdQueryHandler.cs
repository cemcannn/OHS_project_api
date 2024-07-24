using MediatR;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Directorate.GetDirectorateById
{
    public class GetDirectorateByIdQueryHandler : IRequestHandler<GetDirectorateByIdQueryRequest, GetDirectorateByIdQueryResponse>
    {
        readonly IDirectorateReadRepository _directorateReadRepository;

        public GetDirectorateByIdQueryHandler(IDirectorateReadRepository directorateReadRepository)
        {
            _directorateReadRepository = directorateReadRepository;
        }

        public async Task<GetDirectorateByIdQueryResponse> Handle(GetDirectorateByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Retrieve the Directorate by Id
            var directorate = await _directorateReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = directorate.Id.ToString(),
                Name = directorate.Name
            };
        }
    }
}
