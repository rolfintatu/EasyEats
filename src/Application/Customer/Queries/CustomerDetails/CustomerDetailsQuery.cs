using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customer.Queries.CustomerDetails
{
    public class CustomerDetailsQuery : IRequest<CustomerDetailsDto>
    {
        public string UserId { get; set; }
    }

    public class CustomerDetailsHandler : IRequestHandler<CustomerDetailsQuery, CustomerDetailsDto>
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


        public async Task<CustomerDetailsDto> Handle(CustomerDetailsQuery request, CancellationToken cancellationToken)
        {

            var result = await context.Customers.Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync();

            var customer = mapper.Map<CustomerDetailsDto>(result);

            return mapper.Map<CustomerDetailsDto>(customer);

        }
    }
}
