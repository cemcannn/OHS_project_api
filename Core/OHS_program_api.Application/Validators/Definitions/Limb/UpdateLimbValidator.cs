using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.Limb;

namespace OHS_program_api.Application.Validators.Definitions.Limb
{
    public class UpdateLimbValidator : AbstractValidator<VM_Update_Limb>
    {
        public UpdateLimbValidator()
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
        }
    }
}
