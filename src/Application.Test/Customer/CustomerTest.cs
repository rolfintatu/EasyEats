using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Customer.Commands.CreateCustomer;
using Application.Customer.Queries.CustomerDetails;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Entities = Domain.Entities;

namespace Application.Test.Customer
{
    public class CustomerTest
    {

        private Mock<IMediator> mediatR = new Mock<IMediator>();

        [Fact]
        public async void CreateCustomer_Test()
        {

            var requestCustomer = new CreateCustomerCommand(
                id: Guid.NewGuid().ToString(),
                name: "Florin",
                numberPhone: "1459832443"
                );

            var createCustomer = new Entities.Customer(
                id: Guid.NewGuid().ToString(),
                name: "Florin",
                phone: 1459832443,
                null,
                null,
                null
                );

            var mediatR = new Mock<IMediator>();
            var _context = new Mock<IEasyEatsDbContext>();

            mediatR.Setup(x => x.Send(It.IsAny<CreateCustomerCommand>(), default(CancellationToken)))
            .Returns(Task.FromResult(default(Unit)));

            _context.Verify(x => x.Customers.Add(createCustomer), Times.Once);

            var CreateCustomerHandler = new CreateCustomerHandler(_context.Object, mediatR.Object);

            var actual = await CreateCustomerHandler.Handle(requestCustomer, new CancellationToken());

        }

        [Fact]
        public async void GetUserDetails_Test()
        {

            var input = new CustomerDetailsQuery("TestID");

            var mok = new Entities.Customer("TestID", "Florin", 0, null, null, null);

            var output = new CustomerDetailsDto(

                name: "Florin",
                phone: 0762946283,
                address: new Domain.ValueObjects.Address(
                        country: "Romania",
                        city: "Buzau",
                        addressLine: "Com. Valcelele",
                        postalCode: 123452
                    )
            );

            var _context = new Mock<IEasyEatsDbContext>();
            var mapper = new Mock<IMapper>();
            var userService = new Mock<ICurrentUserService>();


            mediatR.Setup(x => x.Send(It.IsAny<CustomerDetailsQuery>(), default(CancellationToken)))
                .Returns(Task.FromResult(output));


            _context.Setup(x => x.Customers.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(mok);

            mapper.Setup(x => x.Map<CustomerDetailsDto>(It.IsAny<Entities.Customer>()))
                .Returns(output);

            var customerDetailsHandler = new CustomerDetailsHandler(_context.Object, userService.Object, mapper.Object);

            var actual = await customerDetailsHandler.Handle(input, new CancellationToken());

            Assert.True(actual != null);
            Assert.Equal(output.Name, actual.Name);

        }

        public List<Entities.Customer> GetSampleCustomers()
        {
            var output = new List<Entities.Customer>()
            {
                new Entities.Customer(
                    id: Guid.NewGuid().ToString(),
                    name: "Florin",
                    phone: 0762946283,
                    address: new Domain.ValueObjects.Address(
                            country: "Romania",
                            city: "Buzau",
                            addressLine: "Com. Valcelele",
                            postalCode: 123452
                        ),
                    reservations: new List<Entities.Reservation>(),
                    orders: new List<Entities.Order>()
                    ),

                 new Entities.Customer(
                    id: Guid.NewGuid().ToString(),
                    name: "Geo",
                    phone: 0762946283,
                    address: new Domain.ValueObjects.Address(
                            country: "Romania",
                            city: "Buzau",
                            addressLine: "Com. Valcelele",
                            postalCode: 123452
                        ),
                    reservations: new List<Entities.Reservation>(),
                    orders: new List<Entities.Order>()
                    ),

                  new Entities.Customer(
                    id: Guid.NewGuid().ToString(),
                    name: "Cata",
                    phone: 0762946283,
                    address: new Domain.ValueObjects.Address(
                            country: "Romania",
                            city: "Buzau",
                            addressLine: "Com. Valcelele",
                            postalCode: 123452
                        ),
                    reservations: new List<Entities.Reservation>(),
                    orders: new List<Entities.Order>()
                    ),
            };

            return output;
        }
    }
}
