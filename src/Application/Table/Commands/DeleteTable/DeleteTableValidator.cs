using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.Table.Commands.DeleteTable
{
    public class DeleteTableValidator : AbstractValidator<DeleteTableCommand>
    {
        public DeleteTableValidator()
        {
        }
    }
}
