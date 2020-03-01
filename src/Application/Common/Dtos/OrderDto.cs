using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class OrderDto : IMapFrom<Entities.Order>
    {

        public OrderDto(){}

        public OrderDto(
            List<Entities.OrderItems> orderItems
            )
        {
            this.OrderItems = orderItems;
            this.Date = DateTime.UtcNow;
        }

        public int Id { get; private set; }
        public DateTime Date { get; private set; } 

        public List<Entities.OrderItems> OrderItems { get; private set; }

        public virtual void Mapping (Profile profile)
        {
            profile.CreateMap<Entities.Order, OrderDto>();
        }
    }

    public class ComplexOrderDto : OrderDto
    {

        public ComplexOrderDto(){}

        public ComplexOrderDto(
            string customerId
            , CustomerDetailsDto customer
            , Bill bill
            , Entities.Reservation reservation
            , List<Entities.OrderItems> orderItems)
            :base(orderItems)
        {
            CustomerId = customerId;
            Customer = customer;
            Bill = bill;
            Reservation = reservation;
        }

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
