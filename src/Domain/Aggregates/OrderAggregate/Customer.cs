using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Aggregates.OrderAggregate
{
    public class Customer : AuditableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; private set; }

        public List<Order> Orders { get; set; }

        public void ChangeAddress(Address newAddress)
            => this.Address = newAddress;
    }
}
