using MediatR;
using OHS_program_api.Application.Abstractions.Services.Safety;
using OHS_program_api.Application.ViewModels.Safety.Accidents;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.UpdateAccident
{
    public class UpdateAccidentCommandHandler : IRequestHandler<UpdateAccidentCommandRequest, UpdateAccidentCommandResponse>
    {
        readonly IAccidentService _accidentService;

        public UpdateAccidentCommandHandler(IAccidentService accidentService)
        {
            _accidentService = accidentService;
        }

        public async Task<UpdateAccidentCommandResponse> Handle(UpdateAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            // Create an instance of VM_Update_Accident with the request data
            var updateAccident = new VM_Update_Accident
            {
                Id = request.Id,
                AccidentDate = request.AccidentDate,
                AccidentHour = request.AccidentHour,
                TypeOfAccident = request.TypeOfAccident,
                AccidentArea = request.AccidentArea,
                Limb = request.Limb,
                Description = request.Description,
                ReportDay = request.ReportDay
            };

            // Pass the VM_Update_Accident instance to UpdateAccidentAsync
            await _accidentService.UpdateAccidentAsync(updateAccident);

            return new UpdateAccidentCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
