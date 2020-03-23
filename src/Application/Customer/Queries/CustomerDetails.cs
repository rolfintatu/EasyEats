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

namespace Application.Customer.Queries
{
    public class CustomerDetails : IRequest<CustomerDetailsDto>
    {
        public CustomerDetails() { }

        public CustomerDetails(string userId)
            => (UserId) = (userId);

        public string UserId { get; set; }
    }

    public class CustomerDetailsHandler : IRequestHandler<CustomerDetails, CustomerDetailsDto>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService currentUser;
        private readonly IMapper mapper;

        public CustomerDetailsHandler(IEasyEatsDbContext context
            , ICurrentUserService currentUser, IMapper mapper)
            => (this.context, this.currentUser, this.mapper)
            = (context, currentUser, mapper);

        public async Task<CustomerDetailsDto> Handle(CustomerDetails request, CancellationToken cancellationToken)
        {
            var result = await context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == currentUser.UserId);

            return mapper.Map<CustomerDetailsDto>(result);
        }
    }
}
