using System;

namespace AppCore.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }

        public OrderDetails OrderDetails { get; set; }
        public Bill Bill { get; set; }
        public DiningTableTrack TableTrack { get; set; }
    }
}