using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class DiningTableTrack
    {
        public int Id { get; private set; }

        public int TableId { get; set; }
        public DiningTable DiningTable { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
