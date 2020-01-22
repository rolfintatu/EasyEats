using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Drink.Commands.CreateDrink
{
    public class CreateDrinkValidator : AbstractValidator<CreateDrinkCommand>
    {
        public CreateDrinkValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.Calories)
                .LessThanOrEqualTo(0);
            RuleFor(x => x.Price)
                .LessThanOrEqualTo(0);
        }
    }
}
