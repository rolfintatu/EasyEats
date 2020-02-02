using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Address
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string AddressLine { get; private set; }
        public int PostalCode { get; private set; }

        public Address(
            string country
            ,string city
            ,string addressLine
            ,int postalCode
            )
        {
            this.Country = country;
            this.City = city;
            this.AddressLine = addressLine;
            this.PostalCode = postalCode;
        }
    }
}
