using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Aggregates.ScheduleAggregate
{
    public class Schedule
    {
        public Guid Id { get; protected set; }
        public Month Month { get; protected set; }
        public List<Reservation> Reservations { get; set; }

        protected Schedule() { }
        private Schedule(Month month)
        {
            Id = Guid.NewGuid();
            Month = month;
            Reservations = new List<Reservation>();
        }

        public static Schedule CreateInstance(DateTime date)
        {
            if (date < DateTime.Now)
                throw new InvalidTimeZoneException();

            return new Schedule(
                    new Month(date.Month, DateTime.DaysInMonth(date.Year, date.Month), date.Year)
                );
        }
    }
}
