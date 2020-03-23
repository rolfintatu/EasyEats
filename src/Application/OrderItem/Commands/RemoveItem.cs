using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderItem.Commands
{
    public class RemoveItem : IRequest
    {

        public RemoveItem(
            int orderItemId
            )
        {
            this.OrderItemId = orderItemId;
        }

        public int OrderItemId { get; private set; }

    }

    public class RemoveItemHandler : IRequestHandler<RemoveItem>
    {
        private readonly IEasyEatsDbContext context;

        public RemoveItemHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }


        public async Task<Unit> Handle(RemoveItem request, CancellationToken cancellationToken)
        {
            var orderItem = await context.OrderItems
                .SingleOrDefaultAsync(x => x.Id == request.OrderItemId);

            if(orderItem.Quantity > 1)
            {
                orderItem.DecreaseQuantity();
            }
            else
            {
                context.OrderItems
                    .Remove(orderItem);
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
