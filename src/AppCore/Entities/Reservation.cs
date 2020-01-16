using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class Reservation
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TableId { get; set; }
        public DiningTable Table { get; set; }
    }
}
