using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class RegistrationModel
    {
        public RegistrationModel(string email, string password, string name, string phoneNumber)
        {
            Email = email;
            Password = password;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
