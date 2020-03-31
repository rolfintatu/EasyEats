using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Customer.Queries;
using AutoMapper;
using MediatR;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;
using Entities = Domain.Entities;

namespace Application.Test.Customer
{
    public class GetCustomerDetails
    {
        private Mock<IMediator> _mediatR = new Mock<IMediator>();
        private Mock<IEasyEatsDbContext> _context = new Mock<IEasyEatsDbContext>();
        private IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
        private Mock<ICurrentUserService> _userService = new Mock<ICurrentUserService>();

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

            var actual = await customerDetailsHandler.Handle(new CustomerDetails(expected.Id), default(CancellationToken));

            Assert.True(actual != null);
            Assert.Equal(expected.Name, actual.Name);

        }
    }
}
