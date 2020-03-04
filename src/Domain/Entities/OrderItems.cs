using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OrderItems : AuditableEntity
    {
        public OrderItems()
        {

        }

        public OrderItems(
            string orderId
            ,int productId
            ,int quantity
            )
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total { get; set; }
        public string OrderId { get; private set; }
        public Order Order { get; set; }

        public int ProductId { get; private set; }
        public Product Product { get; set; }

        public void GetTotal()
        {
            this.Total = this.Quantity * Product.Price;
        }

        public void IncreasQuanity()
        {
            this.Quantity += 1;
        }

        public void DecreaseQuantity()
        {
            this.Quantity -= 1;
        }

    }
}
