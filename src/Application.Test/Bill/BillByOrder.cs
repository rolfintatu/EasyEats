using Entities = Domain.Entities;
using Application.Common.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using MockQueryable.Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Application.Bill.Queries;
using System.Threading.Tasks;

namespace Application.Test.Bill
{
    public class BillByOrder
    {

        private Mock<IEasyEatsDbContext> mokContext = new Mock<IEasyEatsDbContext>();

        [Fact]
        public async Task Test()
        {

            List<Entities.Bill> BillList = new List<Entities.Bill>()
            {
                new Entities.Bill("1Order", new Entities.Order()),
                new Entities.Bill("2Order", new Entities.Order()),
                new Entities.Bill("3Order", new Entities.Order()),
                new Entities.Bill("4Order", new Entities.Order())
            };

            var expect = BillList[1];

            var mokSet = BillList.AsQueryable().BuildMockDbSet();

            mokContext.Setup(x => x.Bills)
                .Returns(mokSet.Object);

            var handler = new BillByOrderHandler(mokContext.Object);

            var request = new Application.Bill.Queries.BillByOrder("2Order");

            var actual = await handler.Handle(request, default(CancellationToken));

            Assert.Equal(expect.OrderId, actual.Bill.OrderId);

        }  

    }
}
