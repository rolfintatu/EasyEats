using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order
    {

        public Order()
            => (Date, OrderItems) = (DateTime.UtcNow, new List<OrderItems>());

        public Order(string customerId, string id)
            => (CustomerId, Id) = (customerId, id);

        public Order(
            List<OrderItems> items, string customerId, string id)
            : this(customerId, id) 
            => (this.OrderItems, this.Date) = (items, DateTime.UtcNow);

        public string Id { get; private set; }
        public DateTime Date { get; private set; }

        public string CustomerId { get; private set; }
        public Customer Customer { get; set; }
        
        public List<OrderItems> OrderItems { get; set; }
    }
}
