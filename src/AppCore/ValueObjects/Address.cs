using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.ValueObjects
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public int PostalCode { get; set; }
    }
}
