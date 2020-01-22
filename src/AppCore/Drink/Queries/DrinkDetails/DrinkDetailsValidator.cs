using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Drink.Queries.DrinkDetails
{
    public class DrinkDetailsValidator : AbstractValidator<DrinkDetailsQuery>
    {
        public DrinkDetailsValidator()
        {
            RuleFor(x => x.Id)
                .LessThanOrEqualTo(0);
        }
    }
}
