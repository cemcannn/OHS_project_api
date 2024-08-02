using MediatR;

namespace OHS_program_api.Application.Features.Commands.Safety.AccidentStatistic.DeleteAccidentStatistic
{
    public class DeleteAccidentStatisticCommandRequest : IRequest<DeleteAccidentStatisticCommandResponse>
    {
        public string Id { get; set; }
    }
}
