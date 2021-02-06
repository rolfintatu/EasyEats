using Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.LogInUser
{
    public class LogInUserModel : IRequest<TokenModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Grant_Type { get; set; }

    }

    public class LogInUserModelValidator : AbstractValidator<LogInUserModel>
    {
        public LogInUserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull().NotEmpty();

            RuleFor(x => x.Grant_Type)
                .NotNull().NotEmpty();
        }
    }
}
