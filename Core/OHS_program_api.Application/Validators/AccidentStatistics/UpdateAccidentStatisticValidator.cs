using FluentValidation;
using OHS_program_api.Application.ViewModels.Safety.AccidentStatistic;

namespace OHS_program_api.Application.Validators.AccidentStatistics
{
    public class UpdateAccidentStatisticValidator : AbstractValidator<VM_Update_AccidentStatistic>
    {
        public UpdateAccidentStatisticValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Kaza istatistik ID boş olamaz.");

            RuleFor(x => x.Month)
                .InclusiveBetween("1", "12")
                    .When(x => !string.IsNullOrEmpty(x.Month))
                    .WithMessage("Ay 1-12 arasında olmalıdır.");

            RuleFor(x => x.Year)
                .Matches(@"^\d{4}$")
                    .When(x => !string.IsNullOrEmpty(x.Year))
                    .WithMessage("Yıl 4 haneli olmalıdır.");

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
