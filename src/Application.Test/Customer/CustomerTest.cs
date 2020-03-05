using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Queries.CustomerDetails;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Domain.Entities;
using Moq.EntityFrameworkCore;
using Moq.EntityFrameworkCore.DbAsyncQueryProvider;
using Application.Common.Mapping;

namespace Application.Test.Customer
{

    public class CustomerTest
    {

        

        private Mock<IMediator> _mediatR = new Mock<IMediator>();
        private Mock<IEasyEatsDbContext> _context = new Mock<IEasyEatsDbContext>();
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        private Mock<ICurrentUserService> _userService = new Mock<ICurrentUserService>();

        [Fact]
        public async void CreateCustomer_Test()
        {

            var requestCustomer = new CreateCustomerCommand(
                id: Guid.NewGuid().ToString(),
                name: "Florin",
                numberPhone: "1459832443"
                );

            _context.Setup(x => x.Customers.AddAsync( It.IsAny<Entities.Customer>(), It.IsAny<CancellationToken>()));

            var CreateCustomerHandler = new CreateCustomerHandler(_context.Object, _mediatR.Object);

            await CreateCustomerHandler.Handle(requestCustomer, new CancellationToken());

            _context.Verify(x => x.Customers.AddAsync(It.IsAny<Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Exactly(1));

            _context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Exactly(1));
        }

        [Fact]
        public async void GetUserDetails_Test()
        {


            var usersList = new List<Entities.Customer>()
            {
                new Entities.Customer("TestID", "Florin", 30, null, null, null),
                new Entities.Customer("TestID1", "George", 0, null, null, null),
                new Entities.Customer("TestID2", "Ion", 0, null, null, null)
            };

            var expected = new Entities.Customer("TestID", "Florin", 30, null, null, null);

            var _mockSet = usersList.AsQueryable().BuildMockDbSet();

            _userService.Setup(x => x.UserId)
                .Returns(expected.Id);

            _context.Setup(x => x.Customers)
                .Returns(_mockSet.Object);


            var customerDetailsHandler = new CustomerDetailsHandler(_context.Object, _userService.Object, mapper);

            var actual = await customerDetailsHandler.Handle(new CustomerDetailsQuery(expected.Id), default(CancellationToken));

            Assert.True(actual != null);
            Assert.Equal(expected.Name, actual.Name);

        }

    }
}
