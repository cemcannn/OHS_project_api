using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.Profession;

namespace OHS_program_api.Application.Validators.Definitions.Profession
{
    public class CreateProfessionValidator : AbstractValidator<VM_Create_Profession>
    {
        public CreateProfessionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen meslek adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Meslek adı 2 ile 100 karakter arası olmalıdır.");
        }
    }
}
