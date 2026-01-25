using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.TypeOfAccident;

namespace OHS_program_api.Application.Validators.Definitions.TypeOfAccident
{
    public class UpdateTypeOfAccidentValidator : AbstractValidator<VM_Update_TypeOfAccident>
    {
        public UpdateTypeOfAccidentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Kaza türü ID boş olamaz.");

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
