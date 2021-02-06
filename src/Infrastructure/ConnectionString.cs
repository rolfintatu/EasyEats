using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class ConnectionString
    {
        public ConnectionString(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
