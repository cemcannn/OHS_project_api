using FluentValidation;
using OHS_program_api.Application.ViewModels.Definitions.Directorate;

namespace OHS_program_api.Application.Validators.Definitions.Directorate
{
    public class CreateDirectorateValidator : AbstractValidator<VM_Create_Directorate>
    {
        public CreateDirectorateValidator()
        {
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
