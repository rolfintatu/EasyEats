using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Entities
{
    public class DiningTable
    {
        public int Id { get; private set; }
        public TableStatus Status { get; set; }
        public int ChairsCount { get; set; }

        public DiningTableTrack TableTruck { get; set; }
        public List<Reservation> Reservations { get; set; }

    }
}
