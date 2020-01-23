using AppCore.ValueObjects;
using System.Collections;
using System.Collections.Generic;

namespace AppCore.Entities
{
    public class Customer
    {

        public Customer()
        {
            this.Reservations = new List<Reservation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}