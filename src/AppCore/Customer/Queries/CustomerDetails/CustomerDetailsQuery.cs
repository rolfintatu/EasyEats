using AppCore.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Customer.Queries.CustomerDetails
{
    public class CustomerDetailsQuery : IRequest<CustomerDetails>
    {
    }

    public class CustomerDetailsHandler : IRequestHandler<CustomerDetailsQuery, CustomerDetails>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IMapper mapper;

        public CustomerDetailsHandler(IEasyEatsDbContext context
            ,ICurrentUserService currentUser
            ,IMapper mapper)
        {
            this.context = context;
            this.currentUser = currentUser;
            this.mapper = mapper;
        }


        public async Task<CustomerDetails> Handle(CustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var customerDetails = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == currentUser.UserId, cancellationToken);

            return mapper.Map<CustomerDetails>(customerDetails);

        }
    }
}
