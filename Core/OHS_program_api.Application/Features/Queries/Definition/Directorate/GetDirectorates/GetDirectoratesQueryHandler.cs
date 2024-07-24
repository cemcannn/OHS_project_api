using MediatR;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.Directorate.GetDirectorates
{
    public class GetDirectoratesQueryHandler : IRequestHandler<GetDirectoratesQueryRequest, GetDirectoratesQueryResponse>
    {
        readonly IDirectorateReadRepository _directorateReadRepository;

        public GetDirectoratesQueryHandler(IDirectorateReadRepository directorateReadRepository)
        {
            _directorateReadRepository = directorateReadRepository;
        }

        public async Task<GetDirectoratesQueryResponse> Handle(GetDirectoratesQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _directorateReadRepository.GetAll(false).Count();
            var directorate = _directorateReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = directorate,
                TotalCount = totalCount
            };
        }
    }
}
