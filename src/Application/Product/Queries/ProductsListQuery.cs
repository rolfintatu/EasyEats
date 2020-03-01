using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Queries
{
    public class ProductsListQuery : IRequest<ProductsListResponse>
    {

        public ProductsListQuery(
            PriceFilter priceFilter
            , Category category
            )
        {
            PriceFilter = priceFilter;
            Category = category;
        }

        public PriceFilter PriceFilter { get; } = PriceFilter.Ascending;
        public Category Category { get; }
    }

    public class ProductsListResponse
    {
        public ProductsListResponse()
        {

        }
        public ProductsListResponse(
            List<Domain.Entities.Product> products
            )
        {
            Products = products;
        }

        public List<Domain.Entities.Product> Products { get; }
    }

    public class ProductsListHandler : IRequestHandler<ProductsListQuery, ProductsListResponse>
    {

        private readonly IEasyEatsDbContext context;

        public ProductsListHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<ProductsListResponse> Handle(ProductsListQuery request, CancellationToken cancellationToken)
        {

            var productsList = new ProductsListResponse();

            switch (request.Category)
            {
                case Category.Food:
                    switch (request.PriceFilter)
                    {
                        case PriceFilter.Ascending:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Food)
                                .OrderBy(x => x.Price).ToListAsync());
                            break;
                        case PriceFilter.Descending:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Food)
                                .OrderByDescending(x => x.Price).ToListAsync());
                            break;
                        default:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking().ToListAsync());
                            break;
                    }
                    break;

                case Category.Drink:
                    switch (request.PriceFilter)
                    {
                        case PriceFilter.Ascending:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Drink)
                                .OrderBy(x => x.Price).ToListAsync());
                            break;
                        case PriceFilter.Descending:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Drink)
                                .OrderByDescending(x => x.Price).ToListAsync());
                            break;
                        default:
                            productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking().ToListAsync());
                            break;
                    }
                    break;
                default:
                    productsList = new ProductsListResponse(
                                await context.Products.AsNoTracking().ToListAsync());
                    break;
            }

            return productsList;
        }
    }
}
