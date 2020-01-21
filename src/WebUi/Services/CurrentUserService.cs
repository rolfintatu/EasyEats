using AppCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUi.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            this.UserId = httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            this.isAuthenticatied = UserId != null;
        }

        public string UserId { get; }
        public bool isAuthenticatied { get; }

    }
}
