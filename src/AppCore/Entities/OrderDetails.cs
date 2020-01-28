using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class OrderDetails
    {

        public OrderDetails()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; private set; }
        public decimal Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public List<Product> Products { get; set; }

    }
}
