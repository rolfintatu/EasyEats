using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with id({key}) was not found.")
        {

        }
    }
}
