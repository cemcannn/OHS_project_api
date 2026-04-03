using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.Directorate.UpdateDirectorate;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.DirectorateRepository;

namespace OHS_program_api.Application.Validators.Definitions.Directorate
{
    public class UpdateDirectorateValidator : AbstractValidator<UpdateDirectorateCommandRequest>
    {
        public UpdateDirectorateValidator(IDirectorateReadRepository directorateReadRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Müdürlük ID boş olamaz.");

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
                    !await DefinitionValidationHelper.HasDuplicateCodeAsync(directorateReadRepository, code, request.Id))
                    .WithMessage("Aynı kodda işletme zaten mevcut.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(directorateReadRepository, name, request.Id))
                    .WithMessage("Aynı isimde işletme zaten mevcut.");
        }
    }
}
