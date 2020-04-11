using Application.Common.Mapping;
using Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class CustomerDetailsDto : IMapFrom<Entities.Customer>
    {

        public CustomerDetailsDto(){}

        public CustomerDetailsDto(string name, int phone, Address address) =>
            (this.Name, this.Phone, this.Address) =(name, phone, address);


        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDetailsDto>();
        }
    }

    public class ComplexCustomerDto : CustomerDetailsDto
    {

        public ComplexCustomerDto() { }

        public ComplexCustomerDto(List<MixReservationDto> reservations, List<OrderDto> orders
            , string name, int phone, Address address)
            : base(name, phone, address) =>
            (this.Reservations, this.Orders) = (reservations, orders);

        public List<MixReservationDto> Reservations { get; set; }
        public List<OrderDto> Orders { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, ComplexCustomerDto>()
                .ForMember(x => x.Reservations, opt => opt.MapFrom(src => src.Reservations))
                .ForMember(x => x.Orders, opt => opt.MapFrom(src => src.Orders));
        }

    }
}
