using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AppCore.Table.Commands.CreateTable
{
    public class CreateTableValidator : AbstractValidator<CreateTableCommand>
    {
        public CreateTableValidator()
        {
        }
    }
}
