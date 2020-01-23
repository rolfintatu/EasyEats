using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Interfaces;
using AppCore.Mapping;
using AppCore.ValueObjects;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Customer.Commands.AddAddress
{
    public class AddAddressCommand : IRequest, IMapFrom<Address>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public int PostalCode { get; set; }

        public  void Mapping(Profile profile)
        {
            profile.CreateMap<AddAddressCommand, Address>();
        }

    }

    public class AddAddressHandler : IRequestHandler<AddAddressCommand>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;
        private readonly ICurrentUserService userService;

        public AddAddressHandler(IEasyEatsDbContext context
            ,IMapper mapper
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<Unit> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(userService.UserId))
            {

            }

            var customer = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == userService.UserId, cancellationToken);

            customer.Address = mapper.Map<Address>(request);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
