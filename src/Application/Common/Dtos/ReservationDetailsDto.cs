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

        public ReservationDetailsDto(DateTime date, TimeSpan startReservation, int duration) 
            => (Date, ReservationStart, Duration) = (date, startReservation, duration);

        public DateTime Date { get; set; }
        public TimeSpan ReservationStart { get; set;  }
        public int Duration { get; set; }
        public string CustomerName { get; set; }

        private void Mapping (Profile profile)
        {
            profile.CreateMap<Reservation, ReservationDetailsDto>();
        }

    }
}
