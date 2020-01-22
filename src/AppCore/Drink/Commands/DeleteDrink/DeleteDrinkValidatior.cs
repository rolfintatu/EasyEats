using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Drink.Commands.DeleteDrink
{
    public class DeleteDrinkValidatior : AbstractValidator<DeleteDrinkCommand>
    {
        public DeleteDrinkValidatior()
        {
            RuleFor(x => x.Id)
                .LessThanOrEqualTo(0);
        }
    }
}
