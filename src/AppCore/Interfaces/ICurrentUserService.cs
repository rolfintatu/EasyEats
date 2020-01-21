using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        bool isAuthenticatied { get; }
    }
}
