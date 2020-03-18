using Application.Common.Interfaces;
using Application.Customer.Commands.CreateCustomer;
using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;
using Entities = Domain.Entities;
using Application.Common.Mapping;

namespace Application.Test.Customer
{

    public class CreateCustomer
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
    }
}
