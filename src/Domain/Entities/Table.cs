using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Table
    {
        public Table()
        {
            this.Reservations = new List<Reservation>();
        }

        public int Id { get; private set; }
        public int ChairsCount { get; set; }
        public TableStatus Status { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
