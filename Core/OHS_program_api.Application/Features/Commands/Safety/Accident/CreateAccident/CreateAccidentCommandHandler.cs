﻿using MediatR;
using OHS_program_api.Application.Abstractions.Services.Safety;

namespace OHS_program_api.Application.Features.Commands.Safety.Accident.CreateAccident
{
    public class CreateAccidentCommandHandler : IRequestHandler<CreateAccidentCommandRequest, CreateAccidentCommandResponse>
    {
        readonly IAccidentService _accidentService;

        public CreateAccidentCommandHandler(IAccidentService accidentService)
        {
            _accidentService = accidentService;
        }

        public async Task<CreateAccidentCommandResponse> Handle(CreateAccidentCommandRequest request, CancellationToken cancellationToken)
        {
            await _accidentService.AddAccidentToPersonnelAsync(new()
            {

                PersonnelId = request.PersonnelId.ToString(),
                TypeOfAccident = request.TypeOfAccident,
                AccidentDate = request.AccidentDate,
                AccidentHour = request.AccidentHour,
                OnTheJobDate = request.OnTheJobDate,
                Description = request.Description

            });

            return new();
        }
    }
}
