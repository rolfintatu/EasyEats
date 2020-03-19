using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities = Domain.Entities;

namespace Application.OrderItem.Queries
{
    public class ItemsByOrder : IRequest<ItemsByOrderResponse>
    {

        public ItemsByOrder(string orderId) 
            => (this.OrderId) = (orderId);

        public string OrderId { get; }
    }

    public class ItemsByOrderResponse
    {
        public ItemsByOrderResponse(IList<OrderItems> items)
        => (this.Items) = (items);

        public IList<Entities.OrderItems> Items { get; private set; }
    }

    public class ItemsByOrderHandler : IRequestHandler<ItemsByOrder, ItemsByOrderResponse>
    {
        private readonly IEasyEatsDbContext context;

        public ItemsByOrderHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<ItemsByOrderResponse> Handle(ItemsByOrder request, CancellationToken cancellationToken)
        {

            if (request.OrderId is null)
            {
                throw new ArgumentNullException(nameof(request.OrderId), "The OrderId must be not null.");
            }

            var itemsList = await context.OrderItems
                .AsNoTracking()
                .Where(x => x.OrderId == request.OrderId)
                .ToListAsync();

            return new ItemsByOrderResponse(itemsList);
        }
    }
}
