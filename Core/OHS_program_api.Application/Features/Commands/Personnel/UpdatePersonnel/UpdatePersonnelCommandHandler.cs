using MediatR;
using OHS_program_api.Application.Abstractions.Services;
using OHS_program_api.Application.ViewModels.Personnel;

namespace OHS_program_api.Application.Features.Commands.Personnel.UpdatePersonnel
{
    public class UpdatePersonnelCommandHandler : IRequestHandler<UpdatePersonnelCommandRequest, UpdatePersonnelCommandResponse>
    {
        readonly IPersonnelService _personnelService;

        public UpdatePersonnelCommandHandler(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public async Task<UpdatePersonnelCommandResponse> Handle(UpdatePersonnelCommandRequest request, CancellationToken cancellationToken)
        {
            // Create an instance of VM_Update_Personnel with the request data
            var updatePersonnel = new VM_Update_Personnel
            {
                Id = request.Id,
                TRIdNumber = request.TRIdNumber,
                TKIId = request.TKIId,
                Name = request.Name,
                Surname = request.Surname,
                BornDate = request.BornDate,
                Profession = request.Profession,
                Directorate = request.Directorate
            };

            // Pass the VM_Update_Personnel instance to UpdatePersonnelAsync
            await _personnelService.UpdatePersonnelAsync(updatePersonnel);

            return new UpdatePersonnelCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
