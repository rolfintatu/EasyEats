using FluentValidation;
using System;

namespace Application.AppReservation.Commands.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.CustonerName)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("You have to priovide a valid customer name.");

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.Now)
                .NotNull()
                .WithMessage("Please enter a valid date.");

            RuleFor(x => x.StartHour)
                .InclusiveBetween(8, 20);

            RuleFor(x => x.StartMinutes)
                .InclusiveBetween(0, 59);

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(30)
                .WithMessage("Must be greater the 30 min.");
                
        }
    }
}
