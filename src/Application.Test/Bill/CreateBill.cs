using Application.Bill.Commands;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MockQueryable.Moq;
using En = Domain.Entities;
using System.Linq;

namespace Application.Test.Bill
{
    public class CreateBill
    {
        private Mock<IEasyEatsDbContext> _context = new Mock<IEasyEatsDbContext>();

        [Fact]
        public async Task CreateBill_Test()
        {
            List<En.Product> products = new List<En.Product>() {
                new En.Product(1, "name", 14, 20, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(2, "name", 22, 40, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(3, "name", 45, 10, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(4, "name", 7, 60, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(5, "name", 15, 40, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(6, "name", 33, 20, Domain.Enums.Category.Drink, 53, ""),
                new En.Product(7, "name", 100, 10, Domain.Enums.Category.Drink, 53, "")
            };

            List<En.OrderItems> orderItems = new List<En.OrderItems>()
            {
                new En.OrderItems("TestOrderId",1,3){ Product =  products[0]},
                new En.OrderItems("TestOrderId",3,6){ Product =  products[2]},
                new En.OrderItems("TestOrderId",4,7){ Product =  products[3]},
                new En.OrderItems("TestOrderId",2,8){ Product =  products[1]},
                new En.OrderItems("TestOrderId",6,2){ Product =  products[5]}
            };

            var _mokSet = products.AsQueryable().BuildMockDbSet();

            _context.Setup(x => x.Bills.AddAsync(It.IsAny<En.Bill>(), default(CancellationToken)));
            _context.Setup(x => x.Products)
                .Returns(_mokSet.Object);

            var handler = new CreateBillHandler(_context.Object);

            var request = new Application.Bill.Commands.CreateBill(new En.Order(orderItems, "TestCustomerId", "TestOrderId"));

            var actual = await handler.Handle(request, default(CancellationToken));

            _context.Verify(x => x.Bills.AddAsync(It.IsAny<En.Bill>(), default(CancellationToken)), Times.Exactly(1));
            _context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Exactly(1));
            
            Assert.NotNull(actual);
            Assert.Equal((decimal)663.3, actual.Bill.Total);
            Assert.Equal((decimal)603, actual.Bill.SubTotal);
            Assert.Equal(17, products.Where(x => x.Id == 1).FirstOrDefault().Quantity);
        }

    }
}
