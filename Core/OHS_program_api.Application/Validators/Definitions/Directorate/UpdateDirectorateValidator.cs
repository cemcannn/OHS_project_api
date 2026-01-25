using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.Directorate;

namespace OHS_program_api.Application.Validators.Definitions.Directorate
{
    public class UpdateDirectorateValidator : AbstractValidator<VM_Update_Directorate>
    {
        public UpdateDirectorateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Müdürlük ID boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen müdürlük adını giriniz.")
                .MaximumLength(100)
                .MinimumLength(2)
                    .WithMessage("Müdürlük adı 2 ile 100 karakter arası olmalıdır.");
        }
    }
}
