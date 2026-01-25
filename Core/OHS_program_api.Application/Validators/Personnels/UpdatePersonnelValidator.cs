using FluentValidation;
using OHS_program_api.Application.ViewModels.Personnel;

namespace OHS_program_api.Application.Validators.Personnels
{
    public class UpdatePersonnelValidator : AbstractValidator<VM_Update_Personnel>
    {
        public UpdatePersonnelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Personel ID boş olamaz.");

            RuleFor(x => x.TRIdNumber)
                .MaximumLength(11)
                .MinimumLength(11)
                    .When(x => !string.IsNullOrEmpty(x.TRIdNumber))
                    .WithMessage("TC Kimlik Numarası 11 haneli olmalıdır.");

            RuleFor(x => x.Name)
                .MaximumLength(30)
                .MinimumLength(2)
                    .When(x => !string.IsNullOrEmpty(x.Name))
                    .WithMessage("İsim 2 ile 30 karakter arası olmalıdır.");

            RuleFor(x => x.Surname)
                .MaximumLength(30)
                .MinimumLength(2)
                    .When(x => !string.IsNullOrEmpty(x.Surname))
                    .WithMessage("Soyisim 2 ile 30 karakter arası olmalıdır.");

            RuleFor(x => x.BornDate)
                .LessThan(DateTime.Now)
                    .When(x => x.BornDate.HasValue)
                    .WithMessage("Doğum tarihi gelecek bir tarih olamaz.");
        }
    }
}
