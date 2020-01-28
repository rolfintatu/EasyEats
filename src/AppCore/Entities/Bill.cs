using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class Bill
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }
        public int Discount { get; set; }
        public int Tax { get; set; }
        public decimal Total { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
