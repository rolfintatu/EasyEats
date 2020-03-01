using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.ValueObjects;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Application.Common.Dtos;
using AutoMapper;
using Domain.Enums;

namespace Application.Order.Queries
{

    public class OrderListResponse {

        public OrderListResponse(
            List<Common.Dtos.OrderDto> orders)
        {
            this.Orders = orders;
        }

        public readonly List<Common.Dtos.OrderDto> Orders;
    }

    public class OrderListQuery : IRequest<OrderListResponse>
    {

        public OrderListQuery(Date date
            ,string userName)
        {
            this.DateFilter = date;
            this.UserName = userName;

        }

        //ByDate
        public Date DateFilter { get; private set; }

        //ByUser
        public string UserName { get; private set; }


    }

    public class OrderListHandler : IRequestHandler<OrderListQuery, OrderListResponse>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;
        private readonly ICurrentUserService userService;

        public OrderListHandler(
            IEasyEatsDbContext context
            ,IMapper mapper
            ,ICurrentUserService userService
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
        }

        public async Task<OrderListResponse> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {

           var response = new OrderListResponse(
                await context.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                .ToListAsync());

            return response;

        }
    }
}
