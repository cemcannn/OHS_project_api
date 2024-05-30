using FluentValidation;
using OHS_program_api.Application.ViewModels.Personnel;

namespace OHS_program_api.Application.Validators.Personnels
{
    public class CreatePersonnelValidator : AbstractValidator<VM_Create_Personnel>
    {
        public CreatePersonnelValidator()
        {
            RuleFor(x => x.TRIdNumber)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen TC Kimlik Numaranızı Boş Geçmeyiniz.")
                .MaximumLength(11)
                .MinimumLength(11)
                    .WithMessage("Lütfen 11 Haneli TC Kimlik Numaranızı Giriniz!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen İsim Alanını Boş Geçmeyiniz.")
                .MaximumLength(30)
                .MinimumLength(2)
                    .WithMessage("Lütfen İsim Alanını 2 ile 30 Karakter Arası Giriniz.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Soyisim Alanını Boş Geçmeyiniz.")
                .MaximumLength(30)
                .MinimumLength(2)
                    .WithMessage("Lütfen Soyisim Alanını 2 ile 30 Karakter Arası Giriniz.");

        }
    }
}
