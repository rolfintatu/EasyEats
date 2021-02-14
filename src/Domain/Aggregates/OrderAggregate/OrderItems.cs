using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.OrderAggregate
{
    public class OrderItems
    {
        public OrderItems(){}

        public OrderItems(string orderId, int productId, int quantity)
            => (OrderId, ProductId, Quantity) = (orderId, productId, quantity);

        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total { get; private set; }
        public string OrderId { get; private set; }
        public Order Order { get; set; }

        public int ProductId { get; private set; }

    }
}
