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
    public class ProductsList : IRequest<ProductsListResponse>
    {

        public ProductsList(PriceFilter priceFilter, Category category)
            => (this.PriceFilter, this.Category) = (priceFilter, category);

        public ProductsList(PriceFilter priceFilter, Category category, int? page)
            : this(priceFilter, category)
            => (this.PriceFilter, this.Category, this.Page) = (priceFilter, category, page);

        public PriceFilter PriceFilter { get; } = PriceFilter.Ascending;
        public Category Category { get; }
        public int? Page { get; } 

    }

    public class ProductsListResponse
    {
        public ProductsListResponse() { }

        public ProductsListResponse(List<Domain.Entities.Product> products)
            => (this.Products) = (products);

        public ProductsListResponse(List<Domain.Entities.Product> products
            , int page, int totalPages)
            : this(products)
            => (this.Page, this.TotalPages) = (page, totalPages);

        public const int ResultsOnPage = 10;

        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        
        public List<Domain.Entities.Product> Products { get; }
    }

    public class ProductsListHandler : IRequestHandler<ProductsList, ProductsListResponse>
    {

        private readonly IEasyEatsDbContext context;

        public ProductsListHandler(IEasyEatsDbContext context)
        => (this.context) = (context);

        public async Task<ProductsListResponse> Handle(ProductsList request, CancellationToken cancellationToken)
        {

            var list = request.Category switch
            {
                Category.Food =>
                    request.PriceFilter switch
                    {
                        PriceFilter.Ascending =>
                                    await context.Products.AsNoTracking()
                                    .Where(x => x.Category == Category.Food)
                                    .OrderBy(x => x.Price).ToListAsync(),
                        PriceFilter.Descending => 
                                    await context.Products.AsNoTracking()
                                    .Where(x => x.Category == Category.Food)
                                    .OrderByDescending(x => x.Price).ToListAsync(),
                        _ => null
                    },

                Category.Drink => 
                    request.PriceFilter switch
                    {
                        PriceFilter.Ascending =>
                                 await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Drink)
                                .OrderBy(x => x.Price).ToListAsync(),
                        PriceFilter.Descending =>
                                await context.Products.AsNoTracking()
                                .Where(x => x.Category == Category.Drink)
                                .OrderByDescending(x => x.Price).ToListAsync(),
                        _ => null
                    },

                _ => null

            };

            ProductsListResponse productsList;

            _ = list.Count() is 0 ? throw new Exception("No products in database!") : productsList = new ProductsListResponse(list);

            var totalResults = productsList.Products.Count();

            var pages =
                totalResults % ProductsListResponse.ResultsOnPage != 0
                ? totalResults / ProductsListResponse.ResultsOnPage + 1
                : totalResults / ProductsListResponse.ResultsOnPage;

            var resultsOnPage = 
                request.Page == pages
                ? totalResults % ProductsListResponse.ResultsOnPage
                : ProductsListResponse.ResultsOnPage ;


            var result = new ProductsListResponse(
                products: productsList.Products.GetRange(
                    (request.Page ?? 1 - 1) * ProductsListResponse.ResultsOnPage, resultsOnPage != productsList.Products.Count ? productsList.Products.Count : resultsOnPage),
                page: request.Page ?? 1,
                totalPages: pages
                );

            return result;
        }
    }
}
