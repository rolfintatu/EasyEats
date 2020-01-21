using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Commands.CreateFood
{
    public class CreateFoodValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).NotEqual(0);
        }
    }
}
