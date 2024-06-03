using MediatR;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.UpdateTypeOfAccident;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;

namespace OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.CreateTypeOfAccident
{
    public class CreateTypeOfAccidentCommandHandler : IRequestHandler<CreateTypeOfAccidentCommandRequest, CreateTypeOfAccidentCommandResponse>
    {
        readonly ITypeOfAccidentWriteRepository _typeOfAccidentWriteRepository;

        public CreateTypeOfAccidentCommandHandler(ITypeOfAccidentWriteRepository typeOfAccidentWriteRepository)
        {
            _typeOfAccidentWriteRepository = typeOfAccidentWriteRepository;
        }

        public async Task<CreateTypeOfAccidentCommandResponse> Handle(CreateTypeOfAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            await _typeOfAccidentWriteRepository.AddAsync(new()
            {
                Name = request.Name,
            });
            await _typeOfAccidentWriteRepository.SaveAsync();

            return new CreateTypeOfAccidentCommandResponse
            {
                Succeeded = true
            };
        }
    }
}
