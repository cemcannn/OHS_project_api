using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.AccidentArea.UpdateAccidentArea;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.AccidentAreaRepository;

namespace OHS_program_api.Application.Validators.Definitions.AccidentArea
{
    public class UpdateAccidentAreaValidator : AbstractValidator<UpdateAccidentAreaCommandRequest>
    {
        public UpdateAccidentAreaValidator(IAccidentAreaReadRepository accidentAreaReadRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Kaza alanı ID boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen kaza alanı adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Kaza alanı adı 2 ile 100 karakter arası olmalıdır.");

            RuleFor(x => x.Description)
                .Must(DefinitionValidationHelper.HasCode)
                    .WithMessage("Lütfen kaza yeri kodunu giriniz.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(accidentAreaReadRepository, name, request.Id))
                    .WithMessage("Aynı isimde kaza yeri zaten mevcut.");

            RuleFor(x => x.Description)
                .MustAsync(async (request, description, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateDescriptionCodeAsync(accidentAreaReadRepository, description, request.Id))
                    .WithMessage("Aynı kodda kaza yeri zaten mevcut.");
        }
    }
}
