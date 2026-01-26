using MediatR;
using Microsoft.EntityFrameworkCore;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Features.Queries.Definition.AccidentArea.GetAccidentAreas
{
    public class GetAccidentAreasQueryHandler : IRequestHandler<GetAccidentAreasQueryRequest, GetAccidentAreasQueryResponse>
    {
        readonly IAccidentAreaReadRepository _accidentAreaReadRepository;

        public GetAccidentAreasQueryHandler(IAccidentAreaReadRepository accidentAreaReadRepository)
        {
            _accidentAreaReadRepository = accidentAreaReadRepository;
        }

        public async Task<GetAccidentAreasQueryResponse> Handle(GetAccidentAreasQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _accidentAreaReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(cancellationToken);

            var accidentArea = await query
                .Select(p => new
                {
                    p.Id,
                    p.Name
                })
                .ToListAsync(cancellationToken);

            return new GetAccidentAreasQueryResponse
            {
                Datas = accidentArea,
                TotalCount = totalCount
            };
        }
    }
}
