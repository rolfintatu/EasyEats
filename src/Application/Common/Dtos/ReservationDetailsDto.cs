using Application.Common.Mapping;
using Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Entities = Domain.Entities;

namespace Application.Common.Dtos
{
    public class ReservationDetailsDto : IMapFrom<Entities.Reservation>
    {
        public Date Date { get; set; }
        public int Hour { get; set;  }
        public int Duration { get; set; }


        public virtual void Mapping (Profile profile)
        {
            profile.CreateMap<Entities.Reservation, ReservationDetailsDto>();
        }

    }

    public class ComplexReservationDto : ReservationDetailsDto
    {
        public CustomerDetailsDto Customer { get; set; }
        public TableDetailsDto Table { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Reservation, ComplexReservationDto>()
                .ForMember(x => x.Customer, x =>x.MapFrom(o => o.Customer));
        }
    }

    public class MixReservationDto : ReservationDetailsDto
    {
        public TableDetailsDto Table { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Reservation, MixReservationDto>()
                .ForMember(x => x.Table, x=>x.MapFrom(opt=>opt.Table));
        }
    }
}
