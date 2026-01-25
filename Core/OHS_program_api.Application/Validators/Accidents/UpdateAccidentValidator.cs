using FluentValidation;
using OHS_program_api.Application.ViewModels.Safety.Accidents;

namespace OHS_program_api.Application.Validators.Accidents
{
    public class UpdateAccidentValidator : AbstractValidator<VM_Update_Accident>
    {
        public UpdateAccidentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Kaza ID boş olamaz.");

            RuleFor(x => x.TypeOfAccident)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen kaza türü seçiniz.");

            RuleFor(x => x.Limb)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen yaralanan uzuv seçiniz.");

            RuleFor(x => x.AccidentDate)
                .LessThanOrEqualTo(DateTime.Now)
                    .When(x => x.AccidentDate.HasValue)
                    .WithMessage("Kaza tarihi gelecek bir tarih olamaz.");

            RuleFor(x => x.AccidentHour)
                .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")
                    .When(x => !string.IsNullOrEmpty(x.AccidentHour))
                    .WithMessage("Lütfen geçerli bir saat formatı giriniz (HH:mm).");

            RuleFor(x => x.LostDayOfWork)
                .GreaterThanOrEqualTo(0)
                    .When(x => x.LostDayOfWork.HasValue)
                    .WithMessage("Kaybedilen iş günü negatif olamaz.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                    .When(x => !string.IsNullOrEmpty(x.Description))
                    .WithMessage("Açıklama en fazla 500 karakter olabilir.");
        }
    }
}
