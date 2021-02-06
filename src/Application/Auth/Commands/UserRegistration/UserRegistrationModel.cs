using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Auth.Commands.UserRegistration
{
    public class UserRegistrationModel : IRequest
    {
        public UserRegistrationModel(string email, string password, string name, string phone)
        {
            Email = email;
            Password = password;
            Name = name;
            Phone = phone;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
