using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var query = _directorateReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var directorate = await query
                .Select(p => new
                {
                    p.Id,
                    p.Name
                })
                .ToListAsync(cancellationToken);

            return new GetDirectoratesQueryResponse
            {
                Datas = directorate,
                TotalCount = totalCount
            };
        }
    }
}
