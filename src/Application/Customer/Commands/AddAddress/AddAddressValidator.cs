using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Customer.Commands.AddAddress
{
    public class AddAddressValidator : AbstractValidator<AddAddressCommand>
    {
        public AddAddressValidator()
        {
        }
    }
}
