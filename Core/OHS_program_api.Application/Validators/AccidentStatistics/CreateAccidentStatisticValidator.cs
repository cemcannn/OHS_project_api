using FluentValidation;
using OHS_program_api.Application.ViewModels.Safety.AccidentStatistic;

namespace OHS_program_api.Application.Validators.AccidentStatistics
{
    public class CreateAccidentStatisticValidator : AbstractValidator<VM_Create_AccidentStatistic>
    {
        public CreateAccidentStatisticValidator()
        {
            RuleFor(x => x.Month)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ay seçiniz.")
                .InclusiveBetween("1", "12")
                    .WithMessage("Ay 1-12 arasında olmalıdır.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen yıl seçiniz.")
                .Matches(@"^\d{4}$")
                    .WithMessage("Yıl 4 haneli olmalıdır.");

            RuleFor(x => x.Directorate)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen müdürlük seçiniz.");

            RuleFor(x => x.ActualDailyWageSurface)
                .Must(value => string.IsNullOrEmpty(value) || (double.TryParse(value, out var result) && result >= 0))
                    .WithMessage("Yüzey günlük yevmiye negatif olamaz.");

            RuleFor(x => x.ActualDailyWageUnderground)
                .Must(value => string.IsNullOrEmpty(value) || (double.TryParse(value, out var result) && result >= 0))
                    .WithMessage("Yeraltı günlük yevmiye negatif olamaz.");

            RuleFor(x => x.EmployeesNumberSurface)
                .Must(value => string.IsNullOrEmpty(value) || (int.TryParse(value, out var result) && result >= 0))
                    .WithMessage("Yüzey çalışan sayısı negatif olamaz.");

            RuleFor(x => x.EmployeesNumberUnderground)
                .Must(value => string.IsNullOrEmpty(value) || (int.TryParse(value, out var result) && result >= 0))
                    .WithMessage("Yeraltı çalışan sayısı negatif olamaz.");
        }
    }
}
