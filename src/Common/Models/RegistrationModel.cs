using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class RegistrationModel
    {
        public RegistrationModel(string id, string email, string password, string name, int phone)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Phone = phone;
        }

        public string Id { get; private set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public int Phone { get; set; }
    }
}
