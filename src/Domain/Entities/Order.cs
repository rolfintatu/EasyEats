using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public OrderDetails OrderDetails { get; set; }
        public Bill Bill { get; set; }
        public Reservation Reservation { get; set; }
    }
}
