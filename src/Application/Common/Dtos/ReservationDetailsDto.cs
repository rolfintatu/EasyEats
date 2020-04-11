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

        public ReservationDetailsDto() { }

        public ReservationDetailsDto(Date date, int hour, int duration) 
            => (Date, Hour, Duration) = (date, hour, duration);

        public Date Date { get; set; }
        public int Hour { get; set;  }
        public int Duration { get; set; }


        private void Mapping (Profile profile)
        {
            profile.CreateMap<Entities.Reservation, ReservationDetailsDto>();
        }

    }

    public class ComplexReservationDto : ReservationDetailsDto
    {

        public ComplexReservationDto() { }

        public ComplexReservationDto(Date date, int hour, int duration)
            : base(date, hour, duration) { }

        public CustomerDetailsDto Customer { get; set; }
        public TableDetailsDto Table { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Reservation, ComplexReservationDto>()
                .ForMember(x => x.Customer, x =>x.MapFrom(o => o.Customer));
        }
    }

    public class MixReservationDto : ReservationDetailsDto
    {

        public MixReservationDto() { }

        public MixReservationDto(Date date, int hour, int duration)
            : base(date, hour, duration) { }

        public TableDetailsDto Table { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Reservation, MixReservationDto>()
                .ForMember(x => x.Table, x => x.MapFrom(x => x.Table));
            
        }
    }
}
