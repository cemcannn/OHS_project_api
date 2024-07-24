using MediatR;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.Features.Commands.Personnel.UpdatePersonnel;
using OHS_program_api.Application.ViewModels.Personnel;

namespace OHS_program_api.Application.Features.Commands.Personnel.CreatePersonnel
{
    public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommandRequest, CreatePersonnelCommandResponse>
    {
        readonly IPersonnelService _personnelService;


        public CreatePersonnelCommandHandler(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public async Task<CreatePersonnelCommandResponse> Handle(CreatePersonnelCommandRequest request, CancellationToken cancellationToken)
        {
            // Create an instance of VM_Update_Personnel with the request data
            var createPersonnel = new VM_Create_Personnel
            {
                TRIdNumber = request.TRIdNumber,
                TKIId = request.TKIId,
                Name = request.Name,
                Surname = request.Surname,
                StartDateOfWork = request.StartDateOfWork,
                Profession = request.Profession,
                Directorate = request.Directorate,
            };

            await _personnelService.AddPersonnelAsync(createPersonnel);

            return new CreatePersonnelCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
