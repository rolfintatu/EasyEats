using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class Month : ValueObject
    {
        private Month() { }

        public Month(int monthNumber, int mountDays, int year)
        {
            MonthNumber = monthNumber;
            MountDays = mountDays;
            Year = year;
        }

        public int MonthNumber { get; set; }
        public int MountDays { get; set; }
        public int Year { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MonthNumber;
            yield return MountDays;
            yield return Year;
        }
    }
}
