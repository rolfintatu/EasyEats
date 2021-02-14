using Application.Common.Mapping;
using Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Aggregates.OrderAggregate;

namespace Application.Common.Dtos
{
    public class CustomerDetailsDto : IMapFrom<Customer>
    {

        public CustomerDetailsDto(){}

        public CustomerDetailsDto(string name, int phone, Address address) =>
            (this.Name, this.Phone, this.Address) =(name, phone, address);


        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerDetailsDto>();
        }
    }
}
