using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Drink.Commands.UpdateDrink
{
    public class UpdateDrinkValidator : AbstractValidator<UpdateDrinkCommand>
    {
        public UpdateDrinkValidator()
        {
            RuleFor(x => x.Id)
                .LessThanOrEqualTo(0);
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
