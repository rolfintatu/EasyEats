using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {

        public Order()
        {
            this.Date = DateTime.UtcNow;
        }

        public Order(string customerId
            , string orderId)
            :this()
        {
            var Id = orderId;
            this.CustomerId = customerId;
            this.Date = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        //public int OrderId { get; set; }
        public DateTime Date { get; private set; }

        public string CustomerId { get; private set; }
        public Customer Customer { get; set; }

        public List<OrderItems> OrderItems { get; set; }

        public Bill Bill { get; set; }
        public Reservation Reservation { get; set; }
    }
}
