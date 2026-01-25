using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.AccidentArea;

namespace OHS_program_api.Application.Validators.Definitions.AccidentArea
{
    public class UpdateAccidentAreaValidator : AbstractValidator<VM_Update_AccidentArea>
    {
        public UpdateAccidentAreaValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Kaza alanı ID boş olamaz.");

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
