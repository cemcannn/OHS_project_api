using MediatR;
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
            var totalCount = _accidentAreaReadRepository.GetAll(false).Count();
            var accidentArea = _accidentAreaReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Name

                }).ToList();

            return new()
            {
                Datas = accidentArea,
                TotalCount = totalCount
            };
        }
    }
}
