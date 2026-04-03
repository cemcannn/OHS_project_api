using FluentValidation;
using OHS_program_api.Application.Features.Commands.Definition.Limb.UpdateLimb;
using OHS_program_api.Application.Helpers;
using OHS_program_api.Application.Repositories.Definition.LimbRepository;

namespace OHS_program_api.Application.Validators.Definitions.Limb
{
    public class UpdateLimbValidator : AbstractValidator<UpdateLimbCommandRequest>
    {
        public UpdateLimbValidator(ILimbReadRepository limbReadRepository)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Uzuv ID boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen uzuv adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Uzuv adı 2 ile 100 karakter arası olmalıdır.");

            RuleFor(x => x.Description)
                .Must(DefinitionValidationHelper.HasCode)
                    .WithMessage("Lütfen uzuv kodunu giriniz.");

            RuleFor(x => x.Name)
                .MustAsync(async (request, name, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateNameAsync(limbReadRepository, name, request.Id))
                    .WithMessage("Aynı isimde uzuv zaten mevcut.");

            RuleFor(x => x.Description)
                .MustAsync(async (request, description, cancellationToken) =>
                    !await DefinitionValidationHelper.HasDuplicateDescriptionCodeAsync(limbReadRepository, description, request.Id))
                    .WithMessage("Aynı kodda uzuv zaten mevcut.");
        }
    }
}
