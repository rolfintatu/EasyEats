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
    public class AddItem : IRequest
    {
        public AddItem(
            string orderId
            , int itemId
            , int quantity
            )
        {
            this.OrderId = orderId;
            this.ItemId = itemId;
            Quantity = quantity;
        }

        public string OrderId { get; private set; }
        public int  ItemId { get; private set; }
        public int Quantity { get; private set; }
    }

    public class AddItemHandler : IRequestHandler<AddItem>
    {
        private readonly IEasyEatsDbContext context;

        public AddItemHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<Unit> Handle(AddItem request, CancellationToken cancellationToken)
        {
            var orderItem = await context.OrderItems
                .SingleOrDefaultAsync(x => x.OrderId == request.OrderId && x.ProductId == request.ItemId);

            if (orderItem is null)
            {
                await context.OrderItems
                    .AddAsync(new Domain.Entities.OrderItems(
                        orderId: request.OrderId,
                        productId: request.ItemId,
                        quantity: request.Quantity
                        ));
            }
            else
            {
                orderItem.IncreasQuanity();
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
