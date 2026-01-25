using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.TypeOfAccident;

namespace OHS_program_api.Application.Validators.Definitions.TypeOfAccident
{
    public class CreateTypeOfAccidentValidator : AbstractValidator<VM_Create_TypeOfAccident>
    {
        public CreateTypeOfAccidentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen kaza türü adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Kaza türü adı 2 ile 100 karakter arası olmalıdır.");
        }
    }
}
