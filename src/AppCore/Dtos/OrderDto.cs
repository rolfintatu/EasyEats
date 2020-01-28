using AppCore.Entities;
using AppCore.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Dtos
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }

        public OrderDetails OrderDetails { get; set; }

        public virtual void Mapping (Profile profile)
        {
            profile.CreateMap<Order, OrderDto>();
        }
    }

    public class ComplexOrderDto : OrderDto
    {
        public string CustomerId { get; set; }
        public CustomerDetailsDto Customer { get; set; }

        public Bill Bill { get; set; }
        public Entities.Reservation Reservation { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDetailsDto>();
        }

    }
}
