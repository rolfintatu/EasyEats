using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class NotValidNumber : Exception
    {
        public NotValidNumber(string propertyName, int number)
            :base($"Number \'{number}\' isn't valid for {propertyName}")
        {
        }

        public NotValidNumber(string propertyName, int number, int max)
            :base($"Number \'{number}\' is invalid for property {propertyName}. Number must be smaller then {max}!")
        {
        }

    }
}
