using Application.Common.Interfaces;
using Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.Commands.UserRegistration
{
    public class UserRegistrationHandler : IRequestHandler<UserRegistrationModel>
    {

        private readonly IIdentityService _identityService;

        public UserRegistrationHandler(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<Unit> Handle(UserRegistrationModel request, CancellationToken cancellationToken)
        {
            await _identityService.Register(
                new RegistrationModel(request.Email, request.Password, request.Name, request.Phone)
                );

            return Unit.Value;
        }
    }
}
