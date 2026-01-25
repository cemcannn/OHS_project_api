using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.AccidentArea;

namespace OHS_program_api.Application.Validators.Definitions.AccidentArea
{
    public class CreateAccidentAreaValidator : AbstractValidator<VM_Create_AccidentArea>
    {
        public CreateAccidentAreaValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen kaza alanı adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Kaza alanı adı 2 ile 100 karakter arası olmalıdır.");
        }
    }
}
