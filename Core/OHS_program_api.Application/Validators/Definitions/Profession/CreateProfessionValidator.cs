using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.Profession.CreateProfession;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.ProfessionRepository;

namespace OHS_program_api.Application.Validators.Definitions.Profession
{
    public class CreateProfessionValidator : AbstractValidator<CreateProfessionCommandRequest>
    {
        public CreateProfessionValidator(IProfessionReadRepository professionReadRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen meslek adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Meslek adı 2 ile 100 karakter arası olmalıdır.");

            RuleFor(x => x.Description)
                .Must(DefinitionValidationHelper.HasCode)
                    .WithMessage("Lütfen meslek kodunu giriniz.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(professionReadRepository, name))
                    .WithMessage("Aynı isimde meslek zaten mevcut.");

            RuleFor(x => x.Description)
                .MustAsync(async (request, description, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateDescriptionCodeAsync(professionReadRepository, description))
                    .WithMessage("Aynı kodda meslek zaten mevcut.");
        }
    }
}
