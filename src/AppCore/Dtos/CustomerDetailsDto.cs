using AppCore.Mapping;
using AppCore.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Dtos
{
    public class CustomerDetailsDto : IMapFrom<Entities.Customer>
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDetailsDto>();
        }
    }

    public class ComplexCustomerDto : CustomerDetailsDto
    {
        public List<MixReservationDto> Reservations { get; set; }
        public List<OrderDto> Orders { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, ComplexCustomerDto>()
                .ForMember(x => x.Reservations, opt => opt.MapFrom(src => src.Reservations))
                .ForMember(x => x.Orders, opt => opt.MapFrom(src => src.Orders));
        }

    }
}
