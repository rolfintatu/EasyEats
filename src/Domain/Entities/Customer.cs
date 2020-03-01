using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            this.Reservations = new List<Reservation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; private set; }

        public List<Reservation> Reservations { get; set; }
        public List<Order> Orders { get; set; }

        public void ChangeAddress(Address newAddress)
        {
            this.Address = newAddress;
        }
    }
}
