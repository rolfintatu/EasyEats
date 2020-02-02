using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class OrderDto : IMapFrom<Entities.Order>
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }

        public Entities.OrderDetails OrderDetails { get; set; }

        public virtual void Mapping (Profile profile)
        {
            profile.CreateMap<Entities.Order, OrderDto>();
        }
    }

    public class ComplexOrderDto : OrderDto
    {
        public string CustomerId { get; set; }
        public CustomerDetailsDto Customer { get; set; }

        public Entities.Bill Bill { get; set; }
        public Entities.Reservation Reservation { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDetailsDto>();
        }

    }
}
