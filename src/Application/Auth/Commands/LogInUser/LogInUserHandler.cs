using Application.Common.Interfaces;
using Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.LogInUser
{
    public class LogInUserHandler : IRequestHandler<LogInUserModel, TokenModel>
    {
        private readonly IIdentityService _identityService;

        public LogInUserHandler(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<TokenModel> Handle(LogInUserModel request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GetToken(request.Email, request.Password, request.Grant_Type);
            return result;
        }
    }
}
