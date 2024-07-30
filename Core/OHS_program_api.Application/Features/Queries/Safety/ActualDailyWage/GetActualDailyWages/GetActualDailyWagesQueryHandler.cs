using MediatR;
using OHS_program_api.Application.Repositories.Safety.ActualWageRepository;

namespace OHS_program_api.Application.Features.Queries.Safety.ActualDailyWage.GetActualDailyWages
{
    public class GetActualDailyWagesQueryHandler : IRequestHandler<GetActualDailyWagesQueryRequest, GetActualDailyWagesQueryResponse>
    {
        readonly IActualDailyWageReadRepository _actualDailyWageReadRepository;

        public GetActualDailyWagesQueryHandler(IActualDailyWageReadRepository actualDailyWageReadRepository)
        {
            _actualDailyWageReadRepository = actualDailyWageReadRepository;
        }

        public async Task<GetActualDailyWagesQueryResponse> Handle(GetActualDailyWagesQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _actualDailyWageReadRepository.GetAll(false).Count();
            var actualDailyWage = _actualDailyWageReadRepository.GetAll(false)
                .Select(p => new
                {
                    p.Id,
                    p.Month,
                    p.Year,
                    p.Directorate,
                    p.ActualWageSurface,
                    p.ActualWageUnderground,
                    p.EmployeesNumberSurface,
                    p.EmployeesNumberUnderground

                }).ToList();

            return new()
            {
                Datas = actualDailyWage,
                TotalCount = totalCount
            };
        }
    }
}
