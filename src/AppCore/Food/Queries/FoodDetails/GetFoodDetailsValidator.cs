using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Food.Queries.FoodDetails
{
    public class GetFoodDetailsValidator : AbstractValidator<FoodDetailsRequest>
    {
        public GetFoodDetailsValidator()
        {
            RuleFor(x => x.Id).NotNull()
                .LessThanOrEqualTo(0).WithMessage("{PropertyName} must not be less then or equal to 0.");
        }
    }
}
