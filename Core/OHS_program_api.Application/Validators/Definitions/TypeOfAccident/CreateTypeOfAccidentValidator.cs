using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.TypeOfAccident.CreateTypeOfAccident;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.TypeOfAccidentRepository;

namespace OHS_program_api.Application.Validators.Definitions.TypeOfAccident
{
    public class CreateTypeOfAccidentValidator : AbstractValidator<CreateTypeOfAccidentCommandRequest>
    {
        public CreateTypeOfAccidentValidator(ITypeOfAccidentReadRepository typeOfAccidentReadRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen kaza türü adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Kaza türü adı 2 ile 100 karakter arası olmalıdır.");

            RuleFor(x => x.Description)
                .Must(DefinitionValidationHelper.HasCode)
                    .WithMessage("Lütfen kaza türü kodunu giriniz.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(typeOfAccidentReadRepository, name))
                    .WithMessage("Aynı isimde kaza türü zaten mevcut.");

            RuleFor(x => x.Description)
                .MustAsync(async (request, description, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateDescriptionCodeAsync(typeOfAccidentReadRepository, description))
                    .WithMessage("Aynı kodda kaza türü zaten mevcut.");
        }
    }
}
