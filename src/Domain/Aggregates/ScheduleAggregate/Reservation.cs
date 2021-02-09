using Domain.ValueObjects;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Aggregates.ScheduleAggregate
{
    public class Reservation : AuditableEntity
    {

        public Reservation(){}

        public Reservation(Date date, int hour, int duration)
            => (Date, Hour, Duration)
            = (date, hour, duration);

        /// <summary>
        /// Minutes.
        /// </summary>
        public const int DefaultDuration = 60;

        public int Id { get; private set; }
        public Date Date { get; private set; }
        public int Hour { get; private set; }
        public int Duration { get; private set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Waiting;

        public string CustomerName { get; set; }
        public int TableNumber { get; set; }
    }
}
