using MediatR;
using OHS_program_api.Application.Features.Queries.Safety.GetAccidents;
using OHS_program_api.Application.Repositories;
using OHS_program_api.Application.ViewModels.Safety.Accidents;

public class GetAccidentByIdQueryHandler : IRequestHandler<GetAccidentByIdQueryRequest, GetAccidentByIdQueryResponse>
{
    readonly IPersonnelReadRepository _personnelReadRepository;

    public GetAccidentByIdQueryHandler(IPersonnelReadRepository personnelReadRepository)
    {
        _personnelReadRepository = personnelReadRepository;
    }

    public async Task<GetAccidentByIdQueryResponse> Handle(GetAccidentByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var totalAccidentCount = 0;
        List<VM_List_Accident> accidents = new List<VM_List_Accident>();

        // Belirli bir Personnel ID'sine sahip personelin Accident özelliklerini getir
        var personnel = await _personnelReadRepository.GetPersonnelWithAccidentsByIdAsync(request.Id, false);

        if (personnel != null)
        {
            totalAccidentCount = personnel.Accident.Count;

            accidents = personnel.Accident
                .Select(a => new VM_List_Accident
                {
                    Id = Convert.ToString(a.Id),
                    PersonnelId = Convert.ToString(a.PersonnelId),
                    TypeOfAccident = a.TypeOfAccident,
                    Limb = a.Limb,
                    AccidentDate = a.AccidentDate,
                    AccidentHour = a.AccidentHour,
                    OnTheJobDate = a.OnTheJobDate,
                    Description = a.Description
                })
                .ToList();
        }

        return new()
        {
            Datas = accidents,
            TotalCount = totalAccidentCount
        };
    }
}
