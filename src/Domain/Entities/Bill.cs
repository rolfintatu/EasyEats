using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Bill
    {
        public Bill()
        {

        }

        public Bill(string orderId)
        {
            this.OrderId = orderId;
        }

        public const int VAT = 10;

        public int Id { get; private set; }
        public DateTime Date { get; private set; }

        //TODO: Discount based on fidelity
        public int Discount { get; set; }

        public decimal SubTotal { get; set; }
        public int Tax { get; private set; } = VAT;
        public decimal Total { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }


        public void ApplyTax()
        {
            this.Total =+ this.SubTotal * VAT / 100;
        }

        public Bill CalculateSubTotal()
        {
            if (!(this.Order is null))
            {
                foreach(OrderItems item in Order.OrderItems)
                {
                    this.SubTotal = + item.Product.Price;
                }
            }
            return this;
        }

    }
}
