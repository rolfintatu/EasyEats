using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public sealed class Generator
    {
        public static string GenerateId()
        {
            var id = new StringBuilder(Guid.NewGuid().ToString());

            id.Replace("-", "");

            return id.ToString();

        }
    }
}
