using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Commands
{
    public class CreateProduct : IRequest
    {
        public CreateProduct(ProductDto product)
        => (Product) = (product);

        public ProductDto Product { get; }
    }

    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Product.Quantity)
                .GreaterThan(0);
        }
    }

    public class CreateProductHandler : IRequestHandler<CreateProduct>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public CreateProductHandler(
            IEasyEatsDbContext context
            , IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProduct request, CancellationToken cancellationToken)
        {

            var ifExist = await context.Products
                .SingleOrDefaultAsync(x => x.Name == request.Product.Name);

            if (ifExist is null)
            {
                await context.Products.AddAsync(
                mapper.Map<Domain.Entities.Product>(request.Product)
                );

                await context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new DuplicateNameException("This product name already exist into database.");
            }
            return Unit.Value;
        }
    }
}
