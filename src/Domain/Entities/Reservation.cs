using Domain.ValueObjects;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reservation
    {
        public int Id { get; private set; }
        public Date Date { get; set; }
        public int Hour { get; set; }
        public int Duration { get; set; }
        public ReservationStatus Status { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

    }
}
