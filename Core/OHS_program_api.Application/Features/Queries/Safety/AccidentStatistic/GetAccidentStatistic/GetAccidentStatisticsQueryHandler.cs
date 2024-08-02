using MediatR;
using OHS_program_api.Application.Repositories.Safety.AccidentStatisticRepository;

namespace OHS_program_api.Application.Features.Queries.Safety.AccidentStatistic.GetAccidentStatistics
{
    public class GetAccidentStatisticsQueryHandler : IRequestHandler<GetAccidentStatisticsQueryRequest, GetAccidentStatisticsQueryResponse>
    {
        readonly IAccidentStatisticReadRepository _accidentStatisticReadRepository;

        public GetAccidentStatisticsQueryHandler(IAccidentStatisticReadRepository accidentStatisticReadRepository)
        {
            _accidentStatisticReadRepository = accidentStatisticReadRepository;
        }

        public async Task<GetAccidentStatisticsQueryResponse> Handle(GetAccidentStatisticsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _accidentStatisticReadRepository.GetAll(false).Count();
            var accidentStatistic = _accidentStatisticReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Month,
                    p.Year,
                    p.Directorate,
                    p.ActualDailyWageSurface,
                    p.ActualDailyWageUnderground,
                    p.EmployeesNumberSurface,
                    p.EmployeesNumberUnderground

                }).ToList();

            return new()
            {
                Datas = accidentStatistic,
                TotalCount = totalCount
            };
        }
    }
}
