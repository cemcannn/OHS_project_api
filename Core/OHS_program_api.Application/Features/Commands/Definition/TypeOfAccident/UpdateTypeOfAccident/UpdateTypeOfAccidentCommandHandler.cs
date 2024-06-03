using MediatR;
using OHS_program_api.Application.Features.Commands.Personnel.UpdatePersonnel;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;


namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident
{
    public class UpdateTypeOfAccidentCommandHandler : IRequestHandler<UpdateTypeOfAccidentCommandRequest, UpdateTypeOfAccidentCommandResponse>
    {
        readonly ITypeOfAccidentWriteRepository _typeOfAccidentWriteRepository;
        readonly ITypeOfAccidentReadRepository _typeOfAccidentReadRepository;

        public UpdateTypeOfAccidentCommandHandler(ITypeOfAccidentWriteRepository typeOfAccidentWriteRepository, ITypeOfAccidentReadRepository typeOfAccidentReadRepository)
        {
            _typeOfAccidentWriteRepository = typeOfAccidentWriteRepository;
            _typeOfAccidentReadRepository = typeOfAccidentReadRepository;
        }

        public async Task<UpdateTypeOfAccidentCommandResponse> Handle(UpdateTypeOfAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            // Create an instance of VM_Update_Personnel with the request data
            Domain.Entities.Definitions.TypeOfAccident? _typeOfAccident = await _typeOfAccidentReadRepository.GetByIdAsync(request.Id);
            if (_typeOfAccident != null)
            {
                _typeOfAccident.Id = new Guid(request.Id);
                _typeOfAccident.Name = request.Name;

                await _typeOfAccidentWriteRepository.SaveAsync();
            }

            return new UpdateTypeOfAccidentCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
