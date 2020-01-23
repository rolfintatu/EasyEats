using AppCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Customer.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest
    {
    }

    public class DeleteAddressHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService userService;

        public DeleteAddressHandler(IEasyEatsDbContext context
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == userService.UserId, cancellationToken);

            customer.Address = null;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
