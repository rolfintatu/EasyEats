using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public ApplicationUser(string userName)
            : base(userName) { }
    }
}
