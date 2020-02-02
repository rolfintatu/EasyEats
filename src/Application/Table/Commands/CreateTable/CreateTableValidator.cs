using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Table.Commands.CreateTable
{
    public class CreateTableValidator : AbstractValidator<CreateTableCommand>
    {
        public CreateTableValidator()
        {
        }
    }
}
