using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.Limb;

namespace OHS_program_api.Application.Validators.Definitions.Limb
{
    public class CreateLimbValidator : AbstractValidator<VM_Create_Limb>
    {
        public CreateLimbValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen uzuv adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Uzuv adı 2 ile 100 karakter arası olmalıdır.");
        }
    }
}
