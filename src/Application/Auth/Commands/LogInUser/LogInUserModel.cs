using Common.Models;
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
}
