using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Commands
{
    public class DecreaseProductQuantity : IRequest
    {
        public DecreaseProductQuantity(
            int productId
            , int quantity
            )
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; }
        public int Quantity { get; }
    }

    public class DecreasProductQuantityHandler : IRequestHandler<DecreaseProductQuantity>
    {
        private readonly IEasyEatsDbContext context;

        public DecreasProductQuantityHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DecreaseProductQuantity request, CancellationToken cancellationToken)
        {

            var product = await context.Products
                .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            product.DecreaseQuantity(request.Quantity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
