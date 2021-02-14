using Application.Common.Mapping;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Dtos
{
    public class OrderDto : IMapFrom<Order>
    {

        public OrderDto(){}

        public OrderDto(
            List<OrderItems> orderItems
            , DateTime dateTime
            )
            => (this.OrderItems, this.Date) = (orderItems, dateTime);

        public int Id { get; private set; }
        public DateTime Date { get; private set; } 

        public List<OrderItems> OrderItems { get; private set; }

        private void Mapping (Profile profile)
        {
            profile.CreateMap<Order, OrderDto>();
        }
    }
}
