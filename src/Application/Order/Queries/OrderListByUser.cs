using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Order.Queries
{
    public class OrderListByUser : IRequest<OrderListByUResponse>
    {
        public OrderListByUser(
            Date date
            ,string UserName
            )
        {
            this.Date = date;
            this.UserName = UserName;

            if (date is null)
            {
                this.Date.ConvertFrom(DateTime.UtcNow);
            }
        }

        public Date Date { get; }
        public string UserName { get; }
    }

    public class OrderListByUResponse
    {
        public OrderListByUResponse(){ }

        public OrderListByUResponse(
            List<OrderDto> orders
            )
        {
            Orders = orders;
        }

        public List<OrderDto> Orders { get; }
    }

    public class OrderListByUHandler : IRequestHandler<OrderListByUser, OrderListByUResponse>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService userService;
        private readonly IMapper mapper;

        public OrderListByUHandler(
              IEasyEatsDbContext context
            , ICurrentUserService userService
            , IMapper mapper
            )
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<OrderListByUResponse> Handle(OrderListByUser request, CancellationToken cancellationToken)
        {

            var response = new OrderListByUResponse();

            if (
                !string.IsNullOrEmpty(request.UserName)
             || !(userService.UserId is null)
               )
            {
                var userId = userService.UserId ?? context.Customers
                        .Where(x => x.Name == request.UserName).FirstOrDefault().Id;

                response = new OrderListByUResponse(
                    await context.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderItems)
                    .Where(x => x.CustomerId == userId
                    && x.Date == new DateTime(
                           request.Date.Year,
                           request.Date.Month,
                           request.Date.Day
                        ))
                    .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                    .ToListAsync()
                    );
            }
            else
            {

                response = new OrderListByUResponse(
                    await context.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderItems)
                    .Where(x => x.Date == new DateTime(
                        request.Date.Year,
                        request.Date.Month,
                        request.Date.Day
                        ))
                    .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                    .ToListAsync()
                    );
            }

            return response;
        }
    }
}
