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

        public ProductsListQuery(
            PriceFilter priceFilter
            , Category category
            , int page) 
            : this(priceFilter, category)
        {
            Page = page;
        }

        public PriceFilter PriceFilter { get; } = PriceFilter.Ascending;
        public Category Category { get; }

        public int Page { get; }


    }

    public class ProductsListResponse
    {

        public const int ResultsOnPage = 10;

        public ProductsListResponse(List<Domain.Entities.Product> products
            , int page
            , int totalPages
            , int totalResults)
            : this(products)
        {
            Page = page;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }

        public ProductsListResponse(){}
        public ProductsListResponse(
            List<Domain.Entities.Product> products
            )
        {
            Products = products;
        }

        public List<Domain.Entities.Product> Products { get; } = new List<Domain.Entities.Product>(ResultsOnPage);


        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
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

            var resultsOnPage = ProductsListResponse.ResultsOnPage;
            var pages = productsList.Products.Count() / ProductsListResponse.ResultsOnPage;

            if (productsList.Products.Count() % ProductsListResponse.ResultsOnPage != 0)
            {
                pages += 1;
            }

            if (request.Page == pages)
            {
                resultsOnPage = productsList.Products.Count() % ProductsListResponse.ResultsOnPage;
            }



            var result = new ProductsListResponse(
                products: productsList.Products.GetRange(
                    (request.Page - 1) * ProductsListResponse.ResultsOnPage
                    , resultsOnPage
                    ),
                page: request.Page,
                totalPages: pages,
                totalResults: productsList.Products.Count()
                );

            return result;
        }
    }
}
