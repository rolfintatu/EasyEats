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
        public CustomerDetailsQuery() { }

        public CustomerDetailsQuery(string userId)
            => (UserId) = (userId);

        public string UserId { get; set; }
    }

    public class CustomerDetailsHandler : IRequestHandler<CustomerDetailsQuery, CustomerDetailsDto>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IMapper mapper;

        public CustomerDetailsHandler(IEasyEatsDbContext context
            , ICurrentUserService currentUser, IMapper mapper)
            => (this.context, this.currentUser, this.mapper)
            = (context, currentUser, mapper);

        public async Task<CustomerDetailsDto> Handle(CustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == currentUser.UserId);

            return mapper.Map<CustomerDetailsDto>(result);
        }
    }
}
