using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Commands.DeleteFood
{
    public class DeleteFoodValidator : AbstractValidator<DeleteFoodCommand>
    {
        public DeleteFoodValidator()
        {
            RuleFor(x => x.Id)
                .LessThanOrEqualTo(0).WithMessage("{PropertyName} must not be less then or equal to 0.");
        }
    }
}
