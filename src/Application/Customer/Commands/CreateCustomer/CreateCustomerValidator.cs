using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
        }
    }
}
