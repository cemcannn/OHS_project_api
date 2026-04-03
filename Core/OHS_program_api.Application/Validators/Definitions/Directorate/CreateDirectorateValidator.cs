using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.CreateDirectorate;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Validators.Definitions.Directorate
{
    public class CreateDirectorateValidator : AbstractValidator<CreateDirectorateCommandRequest>
    {
        public CreateDirectorateValidator(IDirectorateReadRepository directorateReadRepository)
        {
            RuleFor(x => x.Code)
                .Must(code => !string.IsNullOrWhiteSpace(DefinitionValidationHelper.Normalize(code)))
                    .WithMessage("Lütfen işletme kodunu giriniz.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen müdürlük adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Müdürlük adı 2 ile 100 karakter arası olmalıdır.");

            RuleFor(x => x.Code)
                .MustAsync(async (request, code, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateCodeAsync(directorateReadRepository, code))
                    .WithMessage("Aynı kodda işletme zaten mevcut.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(directorateReadRepository, name))
                    .WithMessage("Aynı isimde işletme zaten mevcut.");
        }
    }
}
