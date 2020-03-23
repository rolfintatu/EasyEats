using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class DateAndUserName
    {

        public DateAndUserName(int day, int month, int year, string userName)
            => (Day, Month, Year, UserName) = (day, month, year, userName);

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; } 
        public string UserName { get; set; }
    }
}
