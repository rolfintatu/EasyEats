using Application.Common.Interfaces;
using Application.Product.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Domain.Entities;

namespace Application.Test.Product
{
    public class ProductQueries
    {
        private Mock<IMediator> _mokMediator = new Mock<IMediator>();
        private Mock<IEasyEatsDbContext> _mokContext = new Mock<IEasyEatsDbContext>();

        [Fact]
        public async void ProductsList_TestPaging()
        {
            ICollection<Entities.Product> products = new List<Domain.Entities.Product>()
            {
                new Domain.Entities.Product("one", 12, 1, Domain.Enums.Category.Drink, 120, ""),
                new Domain.Entities.Product("two", 12, 1, Domain.Enums.Category.Drink, 110, ""),
                new Domain.Entities.Product("three", 12, 1, Domain.Enums.Category.Drink, 153, ""),
                new Domain.Entities.Product("four", 12, 1, Domain.Enums.Category.Drink, 13, ""),
                new Domain.Entities.Product("five", 12, 1, Domain.Enums.Category.Drink, 164, ""),
                new Domain.Entities.Product("six", 12, 1, Domain.Enums.Category.Drink, 120, ""),
                new Domain.Entities.Product("seven", 12, 1, Domain.Enums.Category.Drink, 10, ""),
                new Domain.Entities.Product("eight", 12, 1, Domain.Enums.Category.Drink, 320, ""),
                new Domain.Entities.Product("nine", 12, 1, Domain.Enums.Category.Drink, 160, ""),
                new Domain.Entities.Product("ten", 12, 1, Domain.Enums.Category.Drink, 234, ""),
                new Domain.Entities.Product("eleven", 12, 1, Domain.Enums.Category.Drink, 220, ""),
                new Domain.Entities.Product("twelve", 12, 1, Domain.Enums.Category.Food, 130, ""),
                new Domain.Entities.Product("thirteen", 12, 1, Domain.Enums.Category.Food, 10, ""),
            };

            var request = new ProductsList(
                   Domain.Enums.PriceFilter.Ascending
                   , Domain.Enums.Category.Drink, 1);

            var mockSet = products.AsQueryable().BuildMockDbSet();

            _mokContext
                .SetupSequence(x => x.Products)
                .Returns(mockSet.Object)
                .Returns(mockSet.Object)
                .Returns(mockSet.Object);

            var productsListHandler = new ProductsListHandler(_mokContext.Object);

            var actual = await productsListHandler.Handle(request, new CancellationToken());

            Assert.Equal(
                products.Where(x => x.Category == Category.Drink).Take(10)
                , actual.Products);

            actual = await productsListHandler.Handle(new ProductsList(PriceFilter.Ascending, Category.Drink, 2), default(CancellationToken));

            Assert.Single(actual.Products);


            actual = await productsListHandler.Handle(new ProductsList(PriceFilter.Ascending, Category.Food, 1), default(CancellationToken));

            Assert.Equal(2, actual.Products.Count());

        }
    }
}
