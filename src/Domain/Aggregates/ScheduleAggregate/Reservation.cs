using Domain.ValueObjects;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Aggregates.ScheduleAggregate
{
    public class Reservation
    {
        private Reservation(){}
        private Reservation(DateTime date, int startHour, int duration, 
            string custonerName, Guid scheduleId)
            => (Date, StartHour, Duration, CustomerName, Stage, ScheduleId)
            = (date, startHour, duration, custonerName, ReservationStatus.New, scheduleId);

        /// <summary>
        /// Minutes.
        /// </summary>
        public const int DefaultDuration = 60;

        public Guid Id { get; protected set; } = Guid.NewGuid();
        public Guid ScheduleId { get; protected set; }
        public DateTime Date { get; protected set; }
        public ReservationStatus Stage { get; set; }
        public int StartHour { get; protected set; }
        public int Duration { get; protected set; }
        public string CustomerName { get; protected set; }

        public static Reservation CreateInstance(DateTime date, int startHour, int duration, 
            string custonerName, Guid scheduleId)
        {
            if (date < DateTime.UtcNow)
                throw new ArgumentException();

            if (startHour < 8 || startHour > 20)
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(custonerName))
                throw new ArgumentNullException();

            return new Reservation(date, startHour, duration, custonerName, scheduleId);
        }
    }
}
