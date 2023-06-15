using FluentValidation;

namespace Application.Validations.CustomValidators;

public static class DateTimeValidators
{
   public static IRuleBuilderOptions<T, DateTime> IsDateInThePast<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
   {
      return ruleBuilder.Must(date => date < DateTime.Now)
         .WithMessage("Date must be in the past");
   }
   
    public static IRuleBuilderOptions<T, DateTime> IsDateInTheFuture<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder.Must(date => date > DateTime.Now)
            .WithMessage("Date must be in the future");
    }
    
    public static IRuleBuilderOptions<T, DateTime> AfterSunrise<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        var sunrise = TimeOnly.MinValue.AddHours(6);
        return ruleBuilder.Must(date => TimeOnly.FromDateTime(date) > sunrise)
            .WithMessage("{PropertyName} must be after sunrise. You provided {PropertyValue}");
    }
}