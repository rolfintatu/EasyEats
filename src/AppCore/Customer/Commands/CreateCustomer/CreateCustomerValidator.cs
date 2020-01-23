using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Customer.Commands.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
        }
    }
}
