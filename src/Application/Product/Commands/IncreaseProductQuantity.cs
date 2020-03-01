using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Commands
{
    public class IncreaseProductQuantity :IRequest
    {
        public IncreaseProductQuantity(
            int quantity
            , int productId
            )
        {
            Quantity = quantity;
            ProductId = productId;
        }

        public int Quantity { get; }
        public int ProductId { get; }
    }

    public class IncrementProductQuantityHandler : IRequestHandler<IncreaseProductQuantity>
    {
        private readonly IEasyEatsDbContext context;

        public IncrementProductQuantityHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<Unit> Handle(IncreaseProductQuantity request, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            product.IncreaseQuantity(request.Quantity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
