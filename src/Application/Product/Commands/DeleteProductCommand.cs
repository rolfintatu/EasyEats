using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Commands
{
    public class DeleteProductCommand : IRequest
    {

        public DeleteProductCommand(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; private set; }

    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IEasyEatsDbContext context;

        public DeleteProductHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .SingleOrDefaultAsync(x => x.Id == request.ProductId);

            if (product is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.ProductId);
            }

            context.Products
                .Remove(product);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
