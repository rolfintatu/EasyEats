using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class OrderDetails
    {

        public OrderDetails()
        {
            this.Drinks = new List<Drink>();
            this.Foods = new List<Food>();
        }

        public int Id { get; private set; }
        public decimal Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public List<Drink> Drinks { get; set; }
        public List<Food> Foods { get; set; }

    }
}
