using Application.Common.Mapping;
using Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Aggregates.ScheduleAggregate;

namespace Application.Common.Dtos
{
    public class ReservationDetailsDto : IMapFrom<Reservation>
    {

        public ReservationDetailsDto() { }

        public ReservationDetailsDto(Date date, int hour, int duration) 
            => (Date, Hour, Duration) = (date, hour, duration);

        public Date Date { get; set; }
        public int Hour { get; set;  }
        public int Duration { get; set; }


        private void Mapping (Profile profile)
        {
            profile.CreateMap<Reservation, ReservationDetailsDto>();
        }

    }

    public class ComplexReservationDto : ReservationDetailsDto
    {

        public ComplexReservationDto() { }

        public ComplexReservationDto(Date date, int hour, int duration)
            : base(date, hour, duration) { }

        public CustomerDetailsDto Customer { get; set; }

        private void Mapping(Profile profile)
        {
            profile.CreateMap<Reservation, ComplexReservationDto>();
        }
    }

    public class MixReservationDto : ReservationDetailsDto
    {

        public MixReservationDto() { }

        public MixReservationDto(Date date, int hour, int duration)
            : base(date, hour, duration) { }


        private void Mapping(Profile profile)
        {
            profile.CreateMap<Reservation, MixReservationDto>();
            
        }
    }
}
