using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Bill : AuditableEntity
    {

        //Constructors
        public Bill(){}

        public Bill(string orderId, Order order)
            => (this.OrderId, this.Order) = (orderId, order);

        public Bill(decimal subTotal, decimal total, string orderId, Order order) 
            : this(orderId, order) 
            => (this.SubTotal, this.Total) = (subTotal, total);

        //Constants
        public const int VAT = 10;

        public int Id { get; private set; }
        public DateTime Date { get; private set; }

        //TODO: Discount based on fidelity
        public int Discount { get; private set; }

        public decimal SubTotal { get; private set; }
        public int Tax { get; private set; } = VAT;
        public decimal Total { get; private set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }

        public Bill ApplyTax()
        {
            this.Total =+ this.SubTotal + (this.SubTotal * VAT / 100);
            return this;
        }

        public void CalculateSubTotal()
        {
            _ = this.Order.OrderItems ?? throw new NullReferenceException();
            foreach(OrderItems item in Order.OrderItems)
            {
                this.SubTotal += item.Total;
            }
        }
    }
}
