using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<dynamic> GetToken(string userName, string password, string grant_type);
        Task Register(RegistrationModel customerModel);
        Task<bool> ConfirmEmail(string userId, string code);
    }
}
