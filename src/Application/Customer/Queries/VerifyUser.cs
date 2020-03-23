using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customer.Queries
{
    public class VerifyUser : IRequest<bool>
    {
        public VerifyUser(string name)
            => (this.Name) = (name);
        public string Name { get; set; }
    }

    public class VerifyUserHandler : IRequestHandler<VerifyUser, bool>
    {
        private readonly IEasyEatsDbContext context;
        public VerifyUserHandler(IEasyEatsDbContext context)
            => (this.context) = (context);

        public async Task<bool> Handle(VerifyUser request, CancellationToken cancellationToken)
        {

            var userInfo = await context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == request.Name);

            if (userInfo is null)
            {
                return false;
            }

            return true;
        }
    }
}
