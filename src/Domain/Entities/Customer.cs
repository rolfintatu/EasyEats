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

        public Customer(
            string id
            , string name
            , int phone
            , Address address
            , List<Reservation> reservations
            , List<Order> orders)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
            Reservations = reservations;
            Orders = orders;
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
