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
    public class ChangeProductPrice : IRequest
    {
        public ChangeProductPrice(
            decimal newPrice
            , int productId
            )
        {
            NewPrice = newPrice;
            ProductId = productId;
        }

        public decimal NewPrice { get; }
        public int ProductId { get; }
    }

    public class ChangeProductPriceHandler : IRequestHandler<ChangeProductPrice>
    {
        private readonly IEasyEatsDbContext context;

        public ChangeProductPriceHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<Unit> Handle(ChangeProductPrice request, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            product.ChangePrice(request.NewPrice);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
