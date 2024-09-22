using MediatR;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.Features.Queries.Safety.AccidentStatistic.GetAccidentStatistics;

public class GetAccidentStatisticsQueryHandler : IRequestHandler<GetAccidentStatisticsQueryRequest, GetAccidentStatisticsQueryResponse>
{
    private readonly IAccidentStatisticService _accidentStatisticService;

    public GetAccidentStatisticsQueryHandler(IAccidentStatisticService accidentStatisticService)
    {
        _accidentStatisticService = accidentStatisticService;
    }

    public async Task<GetAccidentStatisticsQueryResponse> Handle(GetAccidentStatisticsQueryRequest request, CancellationToken cancellationToken)
    {
        var (accidentStatistics, totalCount) = await _accidentStatisticService.GetAllAccidentStatisticsAsync();

        return new GetAccidentStatisticsQueryResponse
        {
            Datas = accidentStatistics,
            TotalCount = totalCount
        };
    }
}
